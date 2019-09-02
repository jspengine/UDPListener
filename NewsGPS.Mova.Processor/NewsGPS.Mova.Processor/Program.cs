using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsGPS.Mova.Core.Common.Gateway;
using NewsGPS.Mova.Core.Common.Gateway.Configuration;
using NewsGPS.Mova.Core.Common.IOC;
using System;

namespace NewsGPS.Mova.Processor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Configure APP
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .Build();


            var services = new ServiceCollection();
            services.AddSerilogServices(configuration);
            services.AddServices();


            //IOptions Pattern - For get app configurations
            services.AddOptions();
            services.Configure<Listener>(configuration.GetSection("Listener"));
            services.Configure<FowardTo>(configuration.GetSection("FowardTo"));

            var serviceProvider = services.BuildServiceProvider();

            //start app
            serviceProvider.GetService<IStartGateway>().Run();



            Console.ReadKey();
            
        }

    }
}
