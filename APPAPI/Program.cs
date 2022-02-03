using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace APPAPI
{
    /// <summary>
    /// main program 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// execution entry point
        /// </summary>
        public static void Main(string[] args)
        {
      

            CreateHostBuilder(args).Build().Run();

        }

        /// <summary>
        /// create host builder
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
             .UseServiceProviderFactory(new AutofacServiceProviderFactory())

                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                    .ConfigureKestrel(serverOptions =>
                    {

                    })
                    .UseStartup<Startup>()
                       .UseSerilog(
                  (hostingContext, loggerConfiguration) =>
                         loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration)
                        );
                });
            
    }
}
