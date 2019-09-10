using Microsoft.Extensions.Options;
using NewsGPS.Mova.Core.Common.Gateway.Configuration;
using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace NewsGPS.Mova.Core.Common.Gateway
{
    public class Gateway : IStartGateway
    {
        private ILogger _logger;
        private IOptions<Listener> _listenersConfig;
        private IOptions<FowardToMova> _fowardToMovaConfig;
        private IOptions<FowardToSing> _fowardToSingConfig;
       

        public Gateway(ILogger logguer, 
            IOptions<Listener> listenersConfig,
            IOptions<FowardToMova> fowardToMovaConfig,
            IOptions<FowardToSing> fowardToSingConfig)
        {
            _logger = logguer;
            _listenersConfig = listenersConfig;
            _fowardToMovaConfig = fowardToMovaConfig;
            _fowardToSingConfig = fowardToSingConfig;
        }

        public void Run()
        {
            var listenersConfiguration = _listenersConfig.Value;
            var listeners = new List<MovaGateway>();

            foreach (var port in listenersConfiguration.Ports)
            {
                var ip = listenersConfiguration.IpAddress;
                listeners.Add(new MovaGateway(_fowardToMovaConfig,_fowardToSingConfig, _logger, ip, port));
            }


            listeners.AsParallel().ForAll(x =>
            {
                var m = string.Format("INICIANDO LISTENER UDP NA MÁQUINA {0} PARA PORTA: {1}", x.Ip, x.Port);
                _logger.Information(m);

                x.StartCapturePacket();
            });
        }
    }
}
