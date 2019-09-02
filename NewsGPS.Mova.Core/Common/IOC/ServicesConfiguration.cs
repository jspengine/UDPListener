using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsGPS.Mova.Core.Common.Gateway;
using Serilog;
using System;

namespace NewsGPS.Mova.Core.Common.IOC
{
    public static class IOCConfiguration
    {
       
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IStartGateway, Gateway.Gateway>();
        }

        public static IServiceCollection AddSerilogServices(this IServiceCollection services, LoggerConfiguration configuration)
        {
            Log.Logger = configuration.CreateLogger();
            AppDomain.CurrentDomain.ProcessExit += (s, e) => Log.CloseAndFlush();
            return services.AddSingleton(Log.Logger);
        }

        public static IServiceCollection AddSerilogServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            
            var configurationLog = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration);

            return services.AddSerilogServices(configurationLog);
        }

    }
}
