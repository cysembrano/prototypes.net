using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        CalculatorService.CalculatorServiceClient client = null;
        public Form1()
        {
            InitializeComponent();
            client = new CalculatorService.CalculatorServiceClient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int numerator = Convert.ToInt32(txtNumerator.Text);
                int denominator = Convert.ToInt32(txtDenominator.Text);
                lblResult.Text = client.Divide(numerator, denominator).ToString();
            }
            catch (FaultException faultException)
            {
                lblResult.Text = faultException.Message;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            client = new CalculatorService.CalculatorServiceClient();
            MessageBox.Show("New instance of the proxy class is created");
        }
    }
}
