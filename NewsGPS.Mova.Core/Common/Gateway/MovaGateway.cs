
using Microsoft.Extensions.Options;
using NewsGPS.Mova.Core.Common.Gateway.Configuration;
using NewsGPS.Mova.Core.Domain.Helpers;
using Serilog;

namespace NewsGPS.Mova.Core.Common.Gateway
{
    sealed class MovaGateway : UdpListener
    {
        private IOptions<FowardTo> _forwardToConfig;

        public MovaGateway(
            IOptions<FowardTo> fowardToConfig,
            ILogger logguer,
            string ipAddrress, 
            int port) 
            : base(logguer, ipAddrress, port)
        {
            _forwardToConfig = fowardToConfig;
        }

        protected override void Forward(string message)
        {
           
            var sendTo = new Options
            {
                IpAdrress = _forwardToConfig.Value.IpAddress,
                PortNumber = _forwardToConfig.Value.Port
            };

            var servidor = string.Format("{0}:{1}", sendTo.IpAdrress, sendTo.PortNumber);

            this.SendTo(sendTo, message);

            _logger.Information("****** Reencaminhado para MOVA no SERVIDOR {servidor} ******", servidor);
            _logger.Information("{message} ", message);


        }


        protected override void Process(string message)
        {
            _logger.Information("*** Enviando Mensagem para o SING ****");
            _logger.Information("{message}", message);
        }

        protected override void SendAck(Options to, string message)
        {
           
            var messagesSplited = message.Split(';');

            var idEquipamento = messagesSplited[messagesSplited.Length - 3];
            var numeroMessage = messagesSplited[messagesSplited.Length - 2];

            var ackMessage = string.Format(">ACK;{0};{1};", idEquipamento, numeroMessage);
            var ack = CheckSumHelper.Calculate(ackMessage);
            var ackToSend = string.Format("{0}*{1}<\r\n", ackMessage, ack);

            //var m = string.Format("******* ACK enviado PARA {0}:{1} ******** ", to.IpAdrress, to.PortNumber);

            _logger.Information("******* ACK enviado PARA {0}:{1} ******** ", to.IpAdrress, to.PortNumber);
            _logger.Information("{ackToSend}", ackToSend);

            this.SendTo(to, ackToSend);
        }

    }
}
