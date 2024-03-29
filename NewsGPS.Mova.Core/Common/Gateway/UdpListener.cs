﻿
using Serilog;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NewsGPS.Mova.Core.Common.Gateway
{
    public abstract class UdpListener : IDisposable
    {
        private const int _bufferSize = 8 * 1024;
        protected Socket _socket;
        private State _state = new State();
        protected EndPoint _endPointFrom = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback _packetReceivedCallback = null;

        protected readonly ILogger _logger;

        public string Ip { get; private set; }
        public int? Port { get; private set; }

        public class State
        {
            public byte[] buffer = new byte[_bufferSize];
        }

        protected abstract void Process(string message);

        protected abstract void SendAck(string message);

        protected abstract void Forward(string message);


        public UdpListener(ILogger logguer, string ipAddrress, int port)
        {
            Ip = ipAddrress;
            Port = port;

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //_socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
           
            //_socket.Ttl = 255;
            _socket.Bind(new IPEndPoint(IPAddress.Parse(ipAddrress), port));

            _logger = logguer;

        }


        protected void SendTo(Options fowardTo, string message)
        {
            if (string.IsNullOrWhiteSpace(fowardTo.IpAdrress)) throw new Exception("Configure um ip de encaminhamento de pacotes.");
            if (!fowardTo.PortNumber.HasValue) throw new Exception("Configure o numero da porta UDP de encaminhamento de pacotes.");

            var messageBuffer = Encoding.ASCII.GetBytes(message);

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            var endPoint = new IPEndPoint(IPAddress.Parse(fowardTo.IpAdrress), fowardTo.PortNumber.Value);

            socket.SendTo(messageBuffer, endPoint);
        }


        public void StartCapturePacket()
        {
            _socket.BeginReceiveFrom(_state.buffer, 0, _bufferSize, SocketFlags.None, ref _endPointFrom, _packetReceivedCallback = (ar) =>
            {
                string message = string.Empty;
                State so = (State)ar.AsyncState;
                int bytes = 0;

                try
                {

                    bytes = _socket.EndReceiveFrom(ar, ref _endPointFrom);
                   
                    message = Encoding.ASCII.GetString(so.buffer, 0, bytes);

                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        _logger.Information("MENSAGEM RECEBIDA: {message}", message);


                        Task.Run(() => 
                        {
                            SendAck(message);
                        });



                        Task.Run(() =>
                        {
                            
                            Forward(message);
                            _logger.Information("REENCAMINHANDO P/ MOVA: {message} ", message);
                        });
                        


                        Task.Run(() =>
                        {
                            Process(message);
                        });

                    }
                    _socket.BeginReceiveFrom(so.buffer, 0, _bufferSize, SocketFlags.None, ref _endPointFrom, _packetReceivedCallback, so);
                }
                catch(Exception ex)
                {
                    _logger.Error(ex,"Ops algo ruim aconteceu.");
                    _socket.BeginReceiveFrom(so.buffer, 0, _bufferSize, SocketFlags.None, ref _endPointFrom, _packetReceivedCallback, so);
                }
            }, _state);
        }



        public void Dispose()
        {
            //_socket.Close();
        }
    }


}
