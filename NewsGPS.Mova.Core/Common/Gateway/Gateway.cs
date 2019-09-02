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
        private IOptions<FowardTo> _fowardToConfig;

        public Gateway(ILogger logguer, 
            IOptions<Listener> listenersConfig,
            IOptions<FowardTo> fowardToConfig)
        {
            _logger = logguer;
            _listenersConfig = listenersConfig;
            _fowardToConfig = fowardToConfig;
        }

        public void Run()
        {
            var listenersConfiguration = _listenersConfig.Value;
            var listeners = new List<MovaGateway>();

            foreach (var port in listenersConfiguration.Ports)
            {
                var ip = listenersConfiguration.IpAddress;
                listeners.Add(new MovaGateway(_fowardToConfig, _logger, ip, port));
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
