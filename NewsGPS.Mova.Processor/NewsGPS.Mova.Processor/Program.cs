using Microsoft.Extensions.DependencyInjection;
using NewsGPS.Mova.Core.Common.Gateway;
using NewsGPS.Mova.Core.Common.IOC;
using System;

namespace NewsGPS.Mova.Processor
{
    class Program
    {
        static void Main(string[] args)
        {

            var services = new ServiceCollection();
            services.AddSerilogServices();
            services.AddServices();
      
            var serviceProvider = services.BuildServiceProvider();

            //Inciando a aplicação
            serviceProvider.GetService<IStartGateway>().Run();



            Console.ReadKey();
            
        }

    }
}
