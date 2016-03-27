using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Convergys.Assist.Sched
{
    public partial class FrmSettings : Form
    {
        public FrmSettings()
        {
            InitializeComponent();
            InitializeSettings();
        }

        private void InitializeSettings()
        {
            this.txtLogFolderPath.Text = Settings.Instance.LogFolderPath;
            this.txtSAConnection.Text = Settings.Instance.SAConnection;
            this.txtRTAConnection.Text = Settings.Instance.RTAConnection;
        }

        private void btnFolderPath_Click(object sender, EventArgs e)
        {
            DialogResult result = this.folderBrowserDialog1.ShowDialog(this);
            if (result == DialogResult.OK)
            {
                this.txtLogFolderPath.Text = folderBrowserDialog1.SelectedPath;
            } 
        }

        private void FrmSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            Settings.Instance.LogFolderPath = this.txtLogFolderPath.Text;
            Logger.Instance.Reload();

            Settings.Instance.RTAConnection = this.txtRTAConnection.Text;
            Settings.Instance.SAConnection = this.txtSAConnection.Text;
            Settings.Instance.Save();

            this.Hide();
            e.Cancel = true;
        }
    }
}
