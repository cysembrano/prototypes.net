using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Microsoft.IdentityModel;

namespace ValueProducer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string ComputeHashSHA1(string input)
        {
            try
            {
                SHA1CryptoServiceProvider sha1Hasher = new SHA1CryptoServiceProvider();
                byte[] hashedDataBytes = sha1Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));
                string digestValue = Convert.ToBase64String(hashedDataBytes);
                return digestValue;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return String.Empty;
            }

        }

        private string CanonicalizeDsig(string input)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = false;
            try
            {
                doc.LoadXml(input);
                XmlDsigC14NTransform trans = new XmlDsigC14NTransform();                
                trans.LoadInput(doc);
                String c14NInput = new StreamReader((Stream)trans.GetOutput(typeof(Stream))).ReadToEnd();
                return c14NInput;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return String.Empty;
            }

        }

        private string CanonicalizeExc(string input)
        {
            XmlDocument doc = new XmlDocument();
            doc.PreserveWhitespace = false;
            try
            {
                doc.LoadXml(input);
                XmlDsigExcC14NTransform trans = new XmlDsigExcC14NTransform();
                trans.LoadInput(doc);
                String c14NInput = new StreamReader((Stream)trans.GetOutput(typeof(Stream))).ReadToEnd();
                return c14NInput;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return String.Empty;
            }

        }


        private string ComputeHMACSHA1_PSHA(string input, string serversecret, string clientsecret, out string outputAlt)
        {
            try
            {
                byte[] signedInfoBytes = Encoding.UTF8.GetBytes(input);

                byte[] binarySecretBytesServer = Convert.FromBase64String(serversecret);
                byte[] binarySecretBytesClient = Convert.FromBase64String(clientsecret);

                byte[] key = KeyGenerator.ComputeCombinedKey(binarySecretBytesClient, binarySecretBytesServer, 256);

                
                HMACSHA1 hmac = new HMACSHA1(key);
                hmac.Initialize();

                byte[] hmacHash = hmac.ComputeHash(signedInfoBytes);
                outputAlt = hmacHash.Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
                string signatureValue = Convert.ToBase64String(hmacHash);
                return signatureValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                outputAlt = string.Empty;
                return string.Empty;
            }
        }

        private string ComputeHMACSHA1(string input, string serversecret, out string outputAlt)
        {            
            try
            {
                byte[] signedInfoBytes = Encoding.UTF8.GetBytes(input);
                
                byte[] binarySecretBytesServer = Convert.FromBase64String(serversecret);

                HMACSHA1 hmac = new HMACSHA1(binarySecretBytesServer);
                hmac.Initialize();

                byte[] hmacHash = hmac.ComputeHash(signedInfoBytes);
                outputAlt = hmacHash.Aggregate("", (s, e) => s + String.Format("{0:x2}", e), s => s);
                string signatureValue = Convert.ToBase64String(hmacHash);
                return signatureValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                outputAlt = string.Empty;
                return string.Empty;
            }
        }

        static byte[] GenerateSaltedHash(byte[] plainText, byte[] salt)
        {
            HashAlgorithm algorithm = new SHA256Managed();

            byte[] plainTextWithSaltBytes =
              new byte[plainText.Length + salt.Length];

            for (int i = 0; i < plainText.Length; i++)
            {
                plainTextWithSaltBytes[i] = plainText[i];
            }
            for (int i = 0; i < salt.Length; i++)
            {
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];
            }

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }

        private void timestampToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            txtBinarySecretServer.Clear();
            txtInput.Text = CanonicalizeDsig(txtInput.Text);
            string output = ComputeHashSHA1(txtInput.Text);
            if (output != String.Empty)
                txtOutput.Text = output;         

        }

        private void signatureInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            txtInput.Text = CanonicalizeExc(txtInput.Text);
            string outputalt;
            string output = ComputeHMACSHA1(txtInput.Text, 
                                            txtBinarySecretServer.Text, 
                                            out outputalt);
            if (output != String.Empty)
                txtOutput.Text = output;

            if (outputalt != String.Empty)
                textBox1.Text = outputalt;


        }

        private void linarizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtInput.Text = txtInput.Text.RemoveLineEndings().RemoveTabs();
        }

        private void hMACSHA1CSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void oAUTHBASEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void hMACSHA1PSHAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtOutput.Clear();
            txtInput.Text = CanonicalizeExc(txtInput.Text);
            string outputalt;
            string output = ComputeHMACSHA1_PSHA(txtInput.Text,
                                            txtBinarySecretServer.Text,
                                            txtBinarySecretClient.Text,
                                            out outputalt);
            if (output != String.Empty)
                txtOutput.Text = output;

            if (outputalt != String.Empty)
                textBox1.Text = outputalt;
        }

        private void hMACSHA1CryptoHelperToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

    }
}
