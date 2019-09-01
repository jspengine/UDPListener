using Serilog;
using System.Collections.Generic;
using System.Linq;

namespace NewsGPS.Mova.Core.Common.Gateway
{
    public class Gateway : IStartGateway
    {
        private ILogger _logger;

        public Gateway(ILogger logguer)
        {
            _logger = logguer;
        }

        public void Run()
        {
            var portsToListem = new int[1] { 9004 };
            var listeners = new List<MovaGateway>();

            foreach (var port in portsToListem)
            {
                //127.0.0.1
                //listeners.Add(new MovaGateway(_logger, "127.0.0.1", port));
                listeners.Add(new MovaGateway(_logger, "192.168.1.151", port));
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
