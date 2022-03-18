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
            services.AddApplicationInsightsTelemetry(Config.instrumentationKey);

            #region Only for Logging in Startup
            var loggerFactory = LoggerFactory.Create(builder =>
                {                    
                    builder.AddApplicationInsights(Config.instrumentationKey);
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
            });

            //Mapping EndPoints and overriding Data Annotations validation
            services.AddControllers().AddFluentValidation(fv =>
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
            services.AddTransient<ILogHelper, LogHelper>();
            services.AddTransient<IMongoClient,MongoClient>(db=>new MongoClient(Config.connectionString));

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
                    //For Conformance error
                    if (JsonConvert.SerializeObject(problemDetails.Errors).ToLower().Contains(Constants.ValidationErrorMessage.PropertyEmptyError.ToLower()) || JsonConvert.SerializeObject(problemDetails.Errors).ToLower().Contains(Constants.ValidationErrorMessage.PropertyMissingError.ToLower()))
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
                endpoints.MapControllers();
            });          
        }

    }
}
