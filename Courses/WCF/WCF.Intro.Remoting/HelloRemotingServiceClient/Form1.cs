using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace HelloRemotingServiceClient
{
    public partial class Form1 : Form
    {
        IHelloRemotingService.IHelloRemotingService client;
        public Form1()
        {
            InitializeComponent();
            TcpChannel channel = new TcpChannel();
            ChannelServices.RegisterChannel(channel);
            client = (IHelloRemotingService.IHelloRemotingService)Activator.GetObject
                (typeof(IHelloRemotingService.IHelloRemotingService), @"tcp://localhost:8080/GetMessage");
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = client.GetMessage(textBox1.Text);
        }
    }
}
