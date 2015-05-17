using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private async void AsyncLoadingProcess()
        {
            
            await Task.Run(()=>SleepProcess());
            MessageBox.Show("Awaited 10 seconds");
        }

        private void SyncLoadingProcess()
        {

            SleepProcess();
            MessageBox.Show("Awaited 10 seconds");
        }


        private void SleepProcess()
        {
            var r = new Random();
            Thread.Sleep(r.Next(10000));
        }

        private void buttonShowAsyncMessage_Click(object sender, EventArgs e)
        {
            AsyncLoadingProcess();
        }

        private void btnShowSyncMessage_Click(object sender, EventArgs e)
        {
            SyncLoadingProcess();
        }
    }
}
