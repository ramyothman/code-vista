using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Common.Entities.MetaDataSchema;

namespace AppBuilder.Connection
{
    public partial class Connect : DevExpress.XtraEditors.XtraForm
    {
        public Connect()
        {
            InitializeComponent();
        }

        bool allowClose = true;
        private Project _currentProject = null;
        public Project CurrentProject
        {
            set
            {
                _currentProject = value;
            }
            get
            {
                return _currentProject;
            }
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (this.Validate())
            {
                try
                {
                    CurrentProject.ServerName = cmbServer.Text;
                    if (cmbAuthentication.SelectedIndex == 1)
                    {
                        CurrentProject.UserName = txtUserName.Text;
                        CurrentProject.Password = txtPassword.Text;
                        CurrentProject.IsWindowsAuthentication = false;
                    }
                    else
                        CurrentProject.IsWindowsAuthentication = true;

                    CurrentProject.CreateDate = System.DateTime.Now;
                    DataType.LoadHashTables(String.Format("{0}\\DataTypesMapper.xml", Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\"))));
                    CurrentProject.DatabaseType = DatabaseTypes.SQLServer;
                    CurrentProject.Connect();
                    CurrentProject.ExtractorManager.DatabaseReaders.GetDatabases(CurrentProject);
                    allowClose = true;
                    this.Close();
                }
                catch (Exception ex)
                {
                    allowClose = false;
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            allowClose = true;
            this.Close();
        }

        private void cmbServer_Validating(object sender, CancelEventArgs e)
        {
            if (cmbServer.Text.Length == 0)
            {
                e.Cancel = true;
                mainErrorProvider.SetError(cmbServer, "Please enter server name or IP");
            }
            else
            {
                e.Cancel = false;
                mainErrorProvider.SetError(cmbServer, "");
            }
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (cmbAuthentication.SelectedIndex == 1)
            {
                if (txtUserName.Text.Length == 0)
                {
                    e.Cancel = true;
                    mainErrorProvider.SetError(txtUserName, "Please enter User name");
                }
                else
                {
                    e.Cancel = false;
                    mainErrorProvider.SetError(txtUserName, "");
                }
            }
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (cmbAuthentication.SelectedIndex == 1)
            {
                if (txtPassword.Text.Length == 0)
                {
                    e.Cancel = true;
                    mainErrorProvider.SetError(txtPassword, "Please enter User name");
                }
                else
                {
                    e.Cancel = false;
                    mainErrorProvider.SetError(txtPassword, "");
                }
            }
        }

        private void Connect_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allowClose)
                e.Cancel = true;
        }

        private void cmbAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            panelControlAccountInfo.Enabled = cmbAuthentication.SelectedIndex == 1;
        }
    }
}