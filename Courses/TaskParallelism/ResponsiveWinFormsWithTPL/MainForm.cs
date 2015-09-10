using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ResponsiveWinFormsWithTPL
{
    public partial class MainForm : Form
    {
        TaskScheduler uiScheduler;
        int count;

        public MainForm()
        {
            InitializeComponent();
            InitializeComponent2();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }        

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            pictureBox1.Show();

            BusinessLayer.ProcessData(count, (message) => UpdateProgressBar1(message));
            ++count;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            pictureBox2.Show();
            progressBar2.Value = 20;

            BusinessLayer.PerformInternalValidationsOnData(new object(), (message, hidePicture) => UpdateProgressBar2(message, hidePicture));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;
            pictureBox3.Show();
            progressBar3.Value = 0;

            BusinessLayer.PrepareTransformationsForProcessing(new object(), (message, hidePicture) => UpdateProgressBar3(message, hidePicture));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            button4.Enabled = false;
            pictureBox4.Show();
            progressBar4.Value = 0;

            BusinessLayer.PrepareLaunchSequenceForData(new object(), (message, hidePicture) => UpdateProgressBar4(message, hidePicture));
        }

        private void UpdateProgressBar1(string statusMessage)
        {
            Task.Factory.StartNew(() =>
                {
                    progressBar1.PerformStep();
                    label1.Text = statusMessage;
                    pictureBox1.Hide();
                    button1.Enabled = true;
                }, CancellationToken.None, TaskCreationOptions.None, uiScheduler);
        }

        private void UpdateProgressBar2(string statusMessage, bool hidePictureBox)
        {
            Task.Factory.StartNew(() =>
            {
                progressBar2.PerformStep();
                label2.Text = statusMessage;
                if (hidePictureBox)
                {
                    pictureBox2.Hide();
                    button2.Enabled = true;
                }
            }, CancellationToken.None, TaskCreationOptions.None, uiScheduler);
        }

        private void UpdateProgressBar3(string statusMessage, bool hidePictureBox)
        {
            Task.Factory.StartNew(() =>
            {             
                progressBar3.PerformStep();
                label3.Text = statusMessage;
                if (hidePictureBox)
                {
                    pictureBox3.Hide();
                    button3.Enabled = true;
                }
            }, CancellationToken.None, TaskCreationOptions.None, uiScheduler);
        }

        private void UpdateProgressBar4(string statusMessage, bool hidePictureBox)
        {
            Task.Factory.StartNew(() =>
            {
                progressBar4.PerformStep();
                label4.Text = statusMessage;
                if (hidePictureBox)
                {
                    pictureBox4.Hide();
                    button4.Enabled = true;
                }
            }, CancellationToken.None, TaskCreationOptions.None, uiScheduler);
        }

        private void InitializeComponent2()
        {
            progressBar1.Maximum = 100;
            progressBar1.Step = 5;
            progressBar1.Value = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.SetState(1);

            progressBar2.Maximum = 100;
            progressBar2.Step = 20;
            progressBar2.Value = 0;
            progressBar2.Style = ProgressBarStyle.Blocks;
            progressBar2.SetState(3);

            progressBar3.Maximum = 100;
            progressBar3.Step = 1;
            progressBar3.Value = 0;
            progressBar3.Style = ProgressBarStyle.Continuous;
            progressBar3.SetState(2);

            progressBar4.Maximum = 100;
            progressBar4.Step = 1;
            progressBar4.Value = 0;
            progressBar4.Style = ProgressBarStyle.Continuous;
            progressBar4.SetState(1);

            pictureBox1.Hide();
            pictureBox2.Hide();
            pictureBox3.Hide();
            pictureBox4.Hide();

            label1.Text = string.Empty;
            label2.Text = string.Empty;
            label3.Text = string.Empty;
            label4.Text = string.Empty;
        }
    }

    public static class ModifyProgressBarColor
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr w, IntPtr l);
        public static void SetState(this ProgressBar pBar, int state)
        {
            SendMessage(pBar.Handle, 1040, (IntPtr)state, IntPtr.Zero);
        }
    }
}
