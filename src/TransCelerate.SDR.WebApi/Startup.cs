using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using TransCelerate.SDR.Core.AppSettings;
using TransCelerate.SDR.Services.Interfaces;
using TransCelerate.SDR.DataAccess.Interfaces;
using TransCelerate.SDR.Services.Services;
using TransCelerate.SDR.DataAccess.Repositories;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.WebApi.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using TransCelerate.SDR.Core.Utilities;

namespace TransCelerate.SDR.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
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
            services.AddApplicationInsightsTelemetry();

            #region Only for Logging in Startup
            var loggerFactory = LoggerFactory.Create(builder =>
                {                    
                    builder.AddApplicationInsights(Config.instrumentationKey);
                });
            ILogger logger = loggerFactory.CreateLogger<Startup>();   
            #endregion

            //Authorization for the APIs
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)            
                    .AddJwtBearer(o =>
                    {
                        o.Audience = Config.clientID;
                        o.Authority = Config.instance+Config.tenantID;                
                    });

            //Swagger
            services.AddSwaggerGen(c =>
            {                
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Transcelerate SDR", Version = "v1" });
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
            
            //Mapping EndPoints
            services.AddControllers();

            //Enabling CORS
            services.AddCors();

            //Dependency Injection of interfaces
            services.AddTransient<IClinicalStudyRepository, ClinicalStudyRepository>();
            services.AddTransient<IClinicalStudyService, ClinicalStudyService>();
            services.AddTransient<ILogHelper, LogHelper>();
            services.AddTransient<IMongoClient,MongoClient>(db=>new MongoClient(Config.connectionString));

            //AutoMapper Profile
            services.AddAutoMapper(typeof(AutoMapperProfies).Assembly);   
            
            //API to use MVC with validation handling and JSON response
            services.AddMvc().AddNewtonsoftJson();            
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    ValidationProblemDetails problemDetails = new ValidationProblemDetails(context.ModelState);
                    var inputs = ((Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext)context).ActionArguments;
                    if (JsonConvert.SerializeObject(problemDetails.Errors).ToLower().Contains(Constants.ValidationErrorMessage.JsonParseError.ToLower()))
                    {
                        logger.LogError($"API Spec Error : {JsonConvert.SerializeObject(problemDetails.Errors)} ; Input: {JsonConvert.SerializeObject(inputs)} ;");
                        return new BadRequestObjectResult(ErrorResponseHelper.BadRequest("Bad Request"));
                    }
                    else if (JsonConvert.SerializeObject(problemDetails.Errors).ToLower().Contains(Constants.ValidationErrorMessage.ConformanceError.ToLower()) || JsonConvert.SerializeObject(problemDetails.Errors).ToLower().Contains(Constants.ValidationErrorMessage.ValidDateError.ToLower()))
                    {
                        logger.LogError($"Conformance Error  : {JsonConvert.SerializeObject(problemDetails.Errors)} ; Input: {JsonConvert.SerializeObject(inputs)} ;");
                        return new BadRequestObjectResult(ErrorResponseHelper.BadRequest(problemDetails.Errors));
                    }
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
                await next();           
                context.Response.Headers.Add("Content-Type", "application/json; charset=utf-8");
                if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
                {                    
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(ErrorResponseHelper.UnAuthorizedAccess()));
                }
                if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    if (String.IsNullOrWhiteSpace(context.Response.Headers["Controller"]))
                    {                        
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(ErrorResponseHelper.NotFound()));
                    }
                }
                if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
                {                    
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(ErrorResponseHelper.GatewayError()));
                }
                if (context.Response.StatusCode == (int)HttpStatusCode.MethodNotAllowed)
                {                    
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(ErrorResponseHelper.MethodNotAllowed()));
                }
                
            });

            //Enable Authenticationa and Authorization for the Endpoints
            app.UseAuthentication();
            app.UseAuthorization();

            //Map Endpoints with authorization
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers().RequireAuthorization();
            });          
        }

    }
}
