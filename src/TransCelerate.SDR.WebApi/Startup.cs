using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using TransCelerate.SDR.Core.AppSettings;
using TransCelerate.SDR.Core.Filters;
using TransCelerate.SDR.Core.Utilities;
using TransCelerate.SDR.Core.Utilities.Common;
using TransCelerate.SDR.Core.Utilities.Helpers;
using TransCelerate.SDR.RuleEngine.Common;
using TransCelerate.SDR.RuleEngineV3;
using TransCelerate.SDR.RuleEngineV4;
using TransCelerate.SDR.WebApi.DependencyInjection;
using TransCelerate.SDR.WebApi.Mappers;

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
            // Api Versioning
            services.AddApiVersioning(o =>
            {
                o.AssumeDefaultVersionWhenUnspecified = false;
                o.ErrorResponses = new VersioningErrorResponseHelper();
                o.ApiVersionReader = ApiVersionReader.Combine(
                 //   new UrlSegmentApiVersionReader(),
                 //new QueryStringApiVersionReader(IdFieldPropertyName.Common.UsdmVersion),
                 new HeaderApiVersionReader(IdFieldPropertyName.Common.UsdmVersion));
            });

            //Swagger          
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v3", new OpenApiInfo { Title = "Transcelerate SDR", Version = "v4" });
                c.SwaggerDoc("v4", new OpenApiInfo { Title = "Transcelerate SDR", Version = "v5" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.CustomSchemaIds(type => type.ToString().Replace($"{Assembly.GetAssembly(typeof(Core.ErrorModels.ErrorModel)).GetName().Name}.", "").Replace("DTO.", "").Replace("DTO", "").Replace("Dto", ""));
            });
            services.AddSwaggerGenNewtonsoftSupport();

            //Mapping EndPoints and overriding Data Annotations validation
            services.AddHttpContextAccessor();
            services.AddControllers(config =>
            {
                config.Filters.Add<ActionFilter>();
            });

            services.AddFluentValidationAutoValidation();
            ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) => string.Concat(memberInfo.Name.Replace(" ", "")[..1]?.ToLower(), memberInfo.Name.Replace(" ", "").AsSpan(1));
            ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            //Enabling CORS
            services.AddCors();

            //Dependency Injection of interfaces
            services.AddApplicationDependencies();

            //AutoMapper Profile                        
            services.AddAutoMapper(typeof(AutoMapperProfilesV3).Assembly);
            services.AddAutoMapper(typeof(AutoMapperProfilesV4).Assembly);
            services.AddAutoMapper(typeof(AutoMapperProfilesV5).Assembly);
            services.AddAutoMapper(typeof(SharedAutoMapperProfiles).Assembly);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfilesV5>();
            });
            config.AssertConfigurationIsValid();

            //API to use MVC with validation handling and JSON response
            services.AddMvc().AddNewtonsoftJson();
            services.AddValidationDependenciesV3();
            services.AddValidationDependenciesV4();

            // Fluent validation for V5 is turned off in favor of the CDISC Rules Engine
            //services.AddValidationDependenciesV5();

            services.AddValidationDependenciesCommon();

            //var serviceProvider = services.BuildServiceProvider();

            //// Resolve IMapper and validate configuration
            //var mapper = serviceProvider.GetService<IMapper>();
            //mapper.ConfigurationProvider.AssertConfigurationIsValid();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var logger = (ILogHelper)context.HttpContext.RequestServices.GetService(typeof(ILogHelper));
                    ApiBehaviourOptionsHelper apiBehaviourOptionsHelper = new(logger);
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint("/swagger/v3/swagger.json", "Transcelerate SDR");
                c.SwaggerEndpoint("/swagger/v4/swagger.json", "Transcelerate SDR");
                ////c.DefaultModelsExpandDepth(-1);
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

                    using (StreamReader reader = new(context.Request.Body))
                    {
                        request = await reader.ReadToEndAsync();
                        context.Request.Body.Position = 0;

                        var logTask = Task.Run(() => logger.LogInformation("Request Body: {request}", request));
                        var actionTask = Task.Run(() => next());
                        await Task.WhenAll(actionTask, logTask); // Adding request logging as Task to execute in parallel along with request                        
                    }
                    if (String.IsNullOrWhiteSpace(context.Response.Headers["Controller"]) && String.IsNullOrWhiteSpace(context.Response.Headers["InvalidInput"]))
                    {
                        response = await HttpContextResponseHelper.Response(context, response);
                        logger.LogInformation("Status Code: {statusCode}; URL: {path}", context.Response.StatusCode, context.Request.Path);
                    }
                }
                catch (Exception ex)
                {
                    if (String.IsNullOrWhiteSpace(context.Response.Headers["Content-Type"]))
                    {
                        if (context.Response?.Headers != null)
                        {
                            context.Response.Headers["Content-Type"] = "application/json";
                        }
                    }
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    logger.LogError("Exception Occurred: {ex}", ex);
                    logger.LogInformation("Status Code: {statusCode}; URL: {path}", context.Response.StatusCode, context.Request.Path);
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(ErrorResponseHelper.ErrorResponseModel(ex), new JsonSerializerSettings { ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver() }));
                }
            });

            //Map Endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
