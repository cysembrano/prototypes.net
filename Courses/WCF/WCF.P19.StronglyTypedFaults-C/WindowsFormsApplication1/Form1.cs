using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using WindowsFormsApplication1.CalculatorService;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        CalculatorService.CalculatorServiceClient client; 
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
            catch (FaultException<DivideByZeroFault> fault)
            {
                lblResult.Text = fault.Detail.Details + " - " + fault.Detail.Error;
            }
        }
    }
}
