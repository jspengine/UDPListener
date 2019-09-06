
using Microsoft.Extensions.Options;
using NewsGPS.Mova.Core.Common.Gateway.Configuration;
using NewsGPS.Mova.Core.Domain.Helpers;
using Serilog;
using System.Text;

namespace NewsGPS.Mova.Core.Common.Gateway
{
    sealed class MovaGateway : UdpListener
    {
        private IOptions<FowardToMova> _forwardToMovaConfig;
        private IOptions<FowardToSing> _fowardToSingConfig;

        public MovaGateway(
            IOptions<FowardToMova> fowardToMovaConfig,
            IOptions<FowardToSing> fowardToSingConfig,
            ILogger logguer,
            string ipAddrress, 
            int port) 
            : base(logguer, ipAddrress, port)
        {
            _forwardToMovaConfig = fowardToMovaConfig;
            _fowardToSingConfig = fowardToSingConfig;
        }

        protected override void Forward(string message)
        {
           
            var sendTo = new Options
            {
                IpAdrress = _forwardToMovaConfig.Value.IpAddress,
                PortNumber = _forwardToMovaConfig.Value.Port
            };

           
            this.SendTo(sendTo, message);

        }


        protected override void Process(string message)
        {
            
            var rgpToFowardForSing = message.ToRGPProtocol();

            if (!string.IsNullOrWhiteSpace(rgpToFowardForSing))
            {
                try
                {
                    var sendTo = new Options
                    {
                        IpAdrress = _fowardToSingConfig.Value.IpAddress,
                        PortNumber = _fowardToSingConfig.Value.Port
                    };

                    this.SendTo(sendTo, rgpToFowardForSing);
                    _logger.Information("REENCAMINHADO P/ SING: {message}", rgpToFowardForSing);


                }
                catch (System.Exception ex)
                {
                    var m = string.Format("ERRO AO ENVIAR PARA O SING: {0}", rgpToFowardForSing);
                    _logger.Error(ex, m);

                }
            }

           
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
