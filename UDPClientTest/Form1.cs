using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UDPClientTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                IPAddress broadcast = IPAddress.Parse("52.11.233.155");

                byte[] sendbuf = Encoding.ASCII.GetBytes(textBox1.Text);
                IPEndPoint ep = new IPEndPoint(broadcast, 4001);

                s.SendTo(sendbuf, ep);

                MessageBox.Show("Mensagem Enviada.");
            }
            catch (Exception ex)
            {
                textBox2.Text = ex.ToString();
                
            }
            
        }
    }
}
