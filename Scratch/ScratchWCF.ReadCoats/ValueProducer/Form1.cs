using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ValueProducer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timestampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            txtBinarySecretServer.Clear();
            try
            {
                SHA1CryptoServiceProvider sha1Hasher = new SHA1CryptoServiceProvider();
                byte[] hashedDataBytes = sha1Hasher.ComputeHash(Encoding.UTF8.GetBytes(txtInput.Text));
                string digestValue = Convert.ToBase64String(hashedDataBytes);
                txtOutput.Text = digestValue;

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void signatureInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            try
            {
                byte[] signedInfoBytes = Encoding.UTF8.GetBytes(txtInput.Text);

                HMACSHA1 hmac = new HMACSHA1();
                byte[] binarySecretBytes = Convert.FromBase64String(txtBinarySecretServer.Text);

                hmac.Key = binarySecretBytes;
                byte[] hmacHash = hmac.ComputeHash(signedInfoBytes);
                string signatureValue = Convert.ToBase64String(hmacHash);
                txtOutput.Text = signatureValue;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void linarizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text.RemoveLineEndings().RemoveTabs();
        }
    }
}
