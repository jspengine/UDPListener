
using Microsoft.Extensions.Options;
using NewsGPS.Mova.Core.Common.Gateway.Configuration;
using NewsGPS.Mova.Core.Domain.Helpers;
using Serilog;
using System.Text;

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

           
            this.SendTo(sendTo, message);

        }


        protected override void Process(string message)
        {
            //_logger.Information("*** Enviando Mensagem para o SING ****");
            //_logger.Information("{message}", message);
        }

        protected override void SendAck(string message)
        {
           
            var messagesSplited = message.Split(';');

            var idEquipamento = messagesSplited[messagesSplited.Length - 3];
            var numeroMessage = messagesSplited[messagesSplited.Length - 2];

            var ackMessage = string.Format(">ACK;{0};{1};", idEquipamento, numeroMessage);
            var ack = CheckSumHelper.Calculate(ackMessage);
            var ackToSend = string.Format("{0}*{1}<\n\r", ackMessage, ack);

            _logger.Information("ACK: {ackToSend}", ackToSend);

            var messageBuffer = Encoding.ASCII.GetBytes(ackToSend);
            _socket.SendTo(messageBuffer, _endPointFrom);


        }

    }
}
