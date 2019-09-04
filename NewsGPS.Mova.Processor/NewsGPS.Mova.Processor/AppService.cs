using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsGPS.Mova.Core.Common.Gateway;
using NewsGPS.Mova.Core.Common.IOC;
using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace NewsGPS.Mova.Processor
{
    public class AppService : ServiceBase
    {
        private readonly ServiceProvider _serviceProvider;

        public AppService()
        {
            ServiceName = "Mova UDP Listener";
            
            //Startup Services
            var configuration = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .Build();


            var services = new ServiceCollection();

            services.AddSerilogServices(configuration);
            services.AddConfiguration(configuration);
            services.AddServices();

            _serviceProvider = services.BuildServiceProvider();

            
        }

        protected override void OnStart(string[] args)
        {

            //start app
            _serviceProvider.GetService<IStartGateway>().Run();

            base.OnStart(args);
        }

        protected override void OnStop()
        {
            base.OnStop();
        }
    }
}
