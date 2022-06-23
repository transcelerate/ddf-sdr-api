using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Reflection;

using TransCelerate.SDR.Core.AppSettings;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.DataAccess.Repositories;
using TransCelerate.SDR.RuleEngine;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.WebApi.Mappers;
using TransCelerate.SDR.Core.DTO.UserGroups;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.Filters;
using Microsoft.AspNetCore.Authorization;

namespace TransCelerate.SDR.WebApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            //Assign Values from appsettings.json at runtime
            StartupLib.SetConstants(configuration);
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {           
            // Application Insights for logs
            services.AddApplicationInsightsTelemetry(Config.InstrumentationKey);

            #region Only for Logging in Startup
            var loggerFactory = LoggerFactory.Create(builder =>
                {                    
                    builder.AddApplicationInsights(Config.InstrumentationKey);
                });
            ILogger logger = loggerFactory.CreateLogger<Startup>();
            #endregion            

            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transcelerate SDR", Version = "v1" });                             
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
                });
            });

            if (_env.IsDevelopment())
            {
                if (!Config.isAuthEnabled)
                    services.AddTransient<IAuthorizationHandler, AllowAnonymousFilter>();
            }

            #region Authorization
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(o =>
                        {
                            o.Audience = Config.Audience;
                            o.Authority = Config.Authority;                            
                        });            
            #endregion

            //Mapping EndPoints and overriding Data Annotations validation
            services.AddControllers(config =>
            {
                config.Filters.Add<ActionFilter>();
            }).AddFluentValidation(fv =>
            {
                fv.DisableDataAnnotationsValidation = true;
                fv.ImplicitlyValidateChildProperties = true;
                fv.ImplicitlyValidateRootCollectionElements = true;

                fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });

            //Enabling CORS
            services.AddCors();

            //Dependency Injection of interfaces
            services.AddTransient<IClinicalStudyRepository, ClinicalStudyRepository>();
            services.AddTransient<IClinicalStudyService, ClinicalStudyService>();
            services.AddTransient<IUserGroupMappingRepository, UserGroupMappingRepository>();
            services.AddTransient<IUserGroupMappingService, UserGroupMappingService>();
            services.AddTransient<ILogHelper, LogHelper>();
            services.AddTransient<IMongoClient,MongoClient>(db=>new MongoClient(Config.ConnectionString));            

            //AutoMapper Profile
            services.AddAutoMapper(typeof(AutoMapperProfies).Assembly);   
            
            //API to use MVC with validation handling and JSON response
            services.AddMvc().AddNewtonsoftJson();
            services.AddValidationDependencies();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    ValidationProblemDetails problemDetails = new ValidationProblemDetails(context.ModelState);
                    var inputs = ((Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext)context).ActionArguments;
                    context.HttpContext.Response.Headers.Add("InvalidInput", "True");
                    //For Conformance error
                    if ((JsonConvert.SerializeObject(problemDetails.Errors).ToLower().Contains(Constants.ValidationErrorMessage.PropertyEmptyError.ToLower()) || JsonConvert.SerializeObject(problemDetails.Errors).ToLower().Contains(Constants.ValidationErrorMessage.PropertyMissingError.ToLower())
                        || JsonConvert.SerializeObject(problemDetails.Errors).ToLower().Contains(Constants.ValidationErrorMessage.SelectAtleastOneGroup.ToLower()) || JsonConvert.SerializeObject(problemDetails.Errors).ToLower().Contains(Constants.ValidationErrorMessage.InvalidPermissionValue.ToLower())
                        || JsonConvert.SerializeObject(problemDetails.Errors).ToLower().Contains(Constants.ValidationErrorMessage.GroupFilterEmptyError.ToLower())) && !JsonConvert.SerializeObject(problemDetails.Errors).ToLower().Contains(Constants.TokenConstants.Username.ToLower()) && !JsonConvert.SerializeObject(problemDetails.Errors).ToLower().Contains(Constants.TokenConstants.Password.ToLower()))
                    {
                        logger.LogError($"Conformance Error  : {JsonConvert.SerializeObject(problemDetails.Errors)} ; Input: {JsonConvert.SerializeObject(inputs)} ;");
                        return new BadRequestObjectResult(ErrorResponseHelper.BadRequest(problemDetails.Errors));
                    }
                    //Other errors
                    else
                    {
                        logger.LogError($"Input Error : {JsonConvert.SerializeObject(problemDetails.Errors)} ; Input: {JsonConvert.SerializeObject(inputs)} ;");
                        return new BadRequestObjectResult(ErrorResponseHelper.BadRequest(problemDetails.Errors,"Invalid Input"));
                    }
                };               
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>        
        /// <param name="logger"></param>        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,ILogger<Startup> logger)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            }
            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => 
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Transcelerate SDR");
                c.DefaultModelsExpandDepth(-1);
            });    

            //Routing
            app.UseHttpsRedirection();
            app.UseRouting();

            //CORS
            app.UseCors(
               options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials()
            );

            //Custom Response for the API HTTP errors
            app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                    string request = string.Empty;
                    string response = string.Empty;
                    response = await HttpContextResponseHelper.Response(context, response);

                    if (String.IsNullOrWhiteSpace(context.Response.Headers["Controller"]) && String.IsNullOrWhiteSpace(context.Response.Headers["InvalidInput"]))
                    {
                        var AuthToken = context.Request.Headers["Authorization"];
                        using (StreamReader reader = new StreamReader(context.Request.Body))
                        {
                            var text = await reader.ReadToEndAsync();
                            if (text != null)
                                request = text.ToString();
                        }
                        logger.LogInformation($"Status Code: {context.Response.StatusCode}; URL: {context.Request.Path}; requestBody : {request}; responseBody : {response};AuthToken: {AuthToken}");
                    }                 
                }
                catch (Exception ex)
                {
                    if (String.IsNullOrWhiteSpace(context.Response.Headers["Content-Type"]))
                        context.Response.Headers.Add("Content-Type", "application/json");
                    string request= string.Empty;
                    var AuthToken = context.Request.Headers["Authorization"];
                    using (StreamReader reader = new StreamReader(context.Request.Body))
                    {
                        var text = await reader.ReadToEndAsync();
                        if (text != null)
                            request = text.ToString();
                    }
                    var response = JsonConvert.SerializeObject(ErrorResponseHelper.ErrorResponseModel(ex));
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    logger.LogError($"Exception Occurred: {ex.Message}");
                    logger.LogInformation($"Status Code: {context.Response.StatusCode}; URL: {context.Request.Path}; requestBody : {request}; responseBody : {response};AuthToken: {AuthToken}");      
                    await context.Response.WriteAsync(response);
                }
            });

            //Enable Authenticationa and Authorization for the Endpoints
            app.UseAuthentication();
            app.UseAuthorization();

            //Map Endpoints with authorization
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });            
        }

    }
}
