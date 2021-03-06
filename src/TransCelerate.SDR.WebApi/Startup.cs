using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.AppSettings;
using TransCelerate.SDR.Core.Filters;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.RuleEngine;
using TransCelerate.SDR.RuleEngineV1;
using TransCelerate.SDR.WebApi.DependencyInjection;
using TransCelerate.SDR.WebApi.Mappers;

namespace TransCelerate.SDR.WebApi
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        //private List<string> invalidErrorResponse =  new();
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
            services.AddApplicationDependencies();           

            //AutoMapper Profile
            services.AddAutoMapper(typeof(AutoMapperProfies).Assembly);   
            services.AddAutoMapper(typeof(AutoMapperProfilesV1).Assembly);   
            services.AddAutoMapper(typeof(SharedAutoMapperProfiles).Assembly);

            //API to use MVC with validation handling and JSON response
            services.AddMvc().AddNewtonsoftJson();             
            services.AddValidationDependencies();
            services.AddValidationDependenciesV1();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var logger = (ILogHelper)context.HttpContext.RequestServices.GetService(typeof(ILogHelper));
                    ApiBehaviourOptionsHelper apiBehaviourOptionsHelper = new ApiBehaviourOptionsHelper(logger);
                    return apiBehaviourOptionsHelper.ModelStateResponse(context);
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
                    string request = string.Empty;
                    string response = string.Empty;
                    context.Request.EnableBuffering();
                    using (StreamReader reader = new StreamReader(context.Request.Body))
                    {                                                
                        if(!context.Request.Path.Value.Contains(Route.Token))
                        {
                            request = await reader.ReadToEndAsync();                            
                            context.Request.Body.Position = 0;                                                                              
                        }
                        var inputArray = SplitStringIntoArrayHelper.SplitString(request, 32000);//since app insights limit is 32768 characters  
                        var logTask = Task.Run(() => inputArray.ForEach(input => logger.LogInformation($"Request Body {inputArray.IndexOf(input)+1}: {input}")));
                        var actionTask = Task.Run(() => next()); 
                        await Task.WhenAll(actionTask, logTask); // Adding request logging as Task to execute in parallel along with request                        
                    }
                    if (String.IsNullOrWhiteSpace(context.Response.Headers["Controller"]) && String.IsNullOrWhiteSpace(context.Response.Headers["InvalidInput"]))
                    {
                        response = await HttpContextResponseHelper.Response(context, response);
                        var AuthToken = context.Request.Headers["Authorization"];
                        logger.LogInformation($"Status Code: {context.Response.StatusCode}; URL: {context.Request.Path}; AuthToken: {AuthToken}");
                    }
                    //else if (!String.IsNullOrWhiteSpace(context.Response.Headers["InvalidInput"]) && context.Response.Headers["InvalidInput"] == "True")
                    //{
                    //    invalidErrorResponse.ForEach(error => logger.LogError(error));
                    //    invalidErrorResponse = new List<string>();
                    //}                                     
                }
                catch (Exception ex)
                {
                    if (String.IsNullOrWhiteSpace(context.Response.Headers["Content-Type"]))
                        context.Response.Headers.Add("Content-Type", "application/json");                                                 
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    logger.LogError($"Exception Occurred: {ex}");
                    logger.LogInformation($"Status Code: {context.Response.StatusCode}; URL: {context.Request.Path}; AuthToken: {context.Request.Headers["Authorization"]}");      
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(ErrorResponseHelper.ErrorResponseModel(ex)));
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
