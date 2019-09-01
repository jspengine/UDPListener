
using NewsGPS.Mova.Core.Domain.Helpers;
using Serilog;

namespace NewsGPS.Mova.Core.Common.Gateway
{
    sealed class MovaGateway : UdpListener
    {
        public MovaGateway(
            ILogger logguer,
            string ipAddrress, 
            int port) 
            : base(logguer, ipAddrress, port)
        {
        }

        protected override void Forward(string message)
        {
            var configToSend = GetServerToSend().Split(':');

            var sendTo = new Options
            {
                IpAdrress = configToSend[0],
                PortNumber = int.Parse(configToSend[1])
            };

            var servidor = string.Format("{0}:{1}", sendTo.IpAdrress, sendTo.PortNumber);

            this.SendTo(sendTo, message);
            _logger.Information("****** Reencaminhado para MOVA no SERVIDOR {servidor} ******", servidor);
            _logger.Information("{message} ", message);


        }

        private string GetServerToSend()
        {
            //Pegar de um arquivo de configuração.
            return "192.168.1.151:9090"; 
        }

        protected override void Process(string message)
        {
            _logger.Information("*** Enviando Mensagem para o SING ****");
            _logger.Information("{message}", message);
        }

        protected override void SendAck(Options to, string message)
        {
           
            var messagesSplited = message.Split(';');

            var idEquipamento = messagesSplited[messagesSplited.Length - 2];
            var numeroMessage = messagesSplited[messagesSplited.Length - 1];

            var ackMessage = string.Format(">ACK;{0};{1};", idEquipamento, numeroMessage);
            var ack = CheckSumHelper.Calculate(ackMessage);
            var ackToSend = string.Format("{0}*{1}<", ackMessage, ack);

            //var m = string.Format("******* ACK enviado PARA {0}:{1} ******** ", to.IpAdrress, to.PortNumber);

            _logger.Information("******* ACK enviado PARA {0}:{1} ******** ", to.IpAdrress, to.PortNumber);
            _logger.Information("{ackToSend}", ackToSend);

            this.SendTo(to, ackToSend);
        }

    }
}
