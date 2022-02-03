using APP.Abstraction.Repositories;
using APP.Abstraction.Services;
using APP.API.graphQL;
using APP.Core.Services;
using APP.Infrastracture.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace APPAPI
{
    /// <summary>
    /// startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// constructor for startup
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            Configuration = configuration;
        }

        /// <summary>
        /// config
        /// </summary>
        public IConfiguration Configuration { get; }


        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            //Register repositories
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IEmployeeRepository,EmployeeRepository>();
            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<MongoContext>();

            services.AddSingleton(System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory() , "File.csv" ));

            //Enable CORS 
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader() );
            });

            //Enable graphql
            services
                .AddGraphQLServer()
                .AddQueryType<Query>();

        

            services.AddControllersWithViews().AddNewtonsoftJson(options=>
            options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore
            ).AddNewtonsoftJson(options=> options.SerializerSettings.ContractResolver=
            new DefaultContractResolver()
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "APPAPI", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

       
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "APPAPI v1"));
            }
            app.UseCors(options => options.AllowAnyOrigin());

            app.UseRouting();

            app.UseAuthorization();

            //docker configurations
            if (env.IsDevelopment()) {
                app.UseHttpsRedirection();
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGraphQL();
            });

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider= new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(),"Photos")
                    ), RequestPath="/Photos"
            }
                );
        }
    }
}
