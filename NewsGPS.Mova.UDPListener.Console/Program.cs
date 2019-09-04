using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsGPS.Mova.Core.Common.Gateway;
using NewsGPS.Mova.Core.Common.IOC;

namespace NewsGPS.Mova.UDPListener.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            //Startup Services
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .Build();


            var services = new ServiceCollection();

            services.AddSerilogServices(configuration);
            services.AddConfiguration(configuration);
            services.AddServices();

            var serviceProvider = services.BuildServiceProvider();

            serviceProvider.GetService<IStartGateway>().Run();

            System.Console.ReadLine();
        }
    }
}
