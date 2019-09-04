using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsGPS.Mova.Core.Common.Gateway;
using NewsGPS.Mova.Core.Common.Gateway.Configuration;
using NewsGPS.Mova.Core.Common.IOC;
using System;
using System.ServiceProcess;

namespace NewsGPS.Mova.Processor
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var movaService = new AppService())
            {
                ServiceBase.Run(movaService);
            }

            



            //Console.ReadKey();
            
        }

    }
}
