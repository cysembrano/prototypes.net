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

namespace Convergys.Assist.Sched
{
    public partial class FrmAgentSchedule : Form
    {
        private FrmSettings _frmSettings;
        private ScheduleProcessor _processor;
        private TaskScheduler _uiScheduler;
        public FrmAgentSchedule()
        {
            _frmSettings = new FrmSettings();
            _processor = new ScheduleProcessor();
            InitializeComponent();
            
        }

        public void EnsureTeamDropDown()
        {
            this.comboBox1.DataSource = new BindingSource(TeamLookup.GetTeams(), null);
            this.comboBox1.DisplayMember = "Value";
            this.comboBox1.ValueMember = "Key";
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _frmSettings.ShowDialog(this);
        }

        private void btnLoadByTeam_Click(object sender, EventArgs e)
        {
            _processor.StartLoadByTeam((int)this.comboBox1.SelectedValue, (message) => AppendToResult(message));
        }

        private void AppendToResult(string message)
        {
            Task.Factory.StartNew(() =>
                {
                    this.txtResult.AppendText(message);
                    this.txtResult.AppendText(Environment.NewLine);
                    this.toolStripStatusLabel1.Text = message;
                }, CancellationToken.None, TaskCreationOptions.None, _uiScheduler);
        }

        private void FrmAgentSchedule_Load(object sender, EventArgs e)
        {
            _uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();
        }

        private void btnLoadEmpIds_Click(object sender, EventArgs e)
        {
            string input = this.txtEmpIds.Text;
            if (!ValidateInput(input))
            {
                Logger.Instance.Warn("EmpId Input Invalid.", AppendToResult);
                return;
            }

            List<int> empIds = new List<int>();
            string[] splitString = input.Split(',');
            foreach (var split in splitString)
            {
                int y;
                if (Int32.TryParse(split, out y))
                    empIds.Add(y);
            }

            _processor.StartLoadByEmpId(empIds, AppendToResult);

        }

        private bool ValidateInput(string text)
        {
            bool result = true;

            string[] splitString = text.Split(',');

            foreach (var split in splitString)
            {
                int y;
                if (!Int32.TryParse(split, out y))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private void reloadTeamsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EnsureTeamDropDown();
        }
    }
}
