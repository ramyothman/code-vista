using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common.Entities.MetaDataSchema;
using System.Xml;
using System.Web.Security;

namespace CodeVista
{
    public partial class LoginForm : Form
    {
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

        public LoginForm()
        {
            InitializeComponent();

        }
        private void LoginForm_Load(object sender, EventArgs e)
        {
            cmbAuthentication.SelectedIndex = 0;
            //Load the last login data that was saved
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            if (System.IO.File.Exists(path+"\\logInfo.xml"))
            {
                XmlReader loginfo = XmlReader.Create(path+"\\logInfo.xml");
                bool done = false;
                while (!done)
                {
                    loginfo.Read();
                    if (loginfo.NodeType == XmlNodeType.Element && loginfo.Name == "LoginInfo")
                    {
                        while (loginfo.NodeType != XmlNodeType.EndElement)
                        {
                            loginfo.Read();
                            if (loginfo.Name == "server")
                            {
                                while (loginfo.NodeType != XmlNodeType.EndElement)
                                {
                                    loginfo.Read();
                                    if (loginfo.NodeType == XmlNodeType.Text)
                                    {
                                        cmbServer.Text = loginfo.Value;
                                    }
                                }
                                loginfo.Read();
                            }
                            if (loginfo.Name == "authentication")
                            {
                                while (loginfo.NodeType != XmlNodeType.EndElement)
                                {
                                    loginfo.Read();
                                    if (loginfo.NodeType == XmlNodeType.Text)
                                    {
                                        cmbAuthentication.SelectedIndex = int.Parse(loginfo.Value);
                                    }
                                }
                                loginfo.Read();
                            }
                            if (loginfo.Name == "username")
                            {
                                while (loginfo.NodeType != XmlNodeType.EndElement)
                                {
                                    loginfo.Read();
                                    if (loginfo.NodeType == XmlNodeType.Text)
                                    {
                                        txtUserName.Text = loginfo.Value;
                                    }
                                }
                                loginfo.Read();
                            }
                            if (loginfo.Name == "password")
                            {
                                while (loginfo.NodeType != XmlNodeType.EndElement)
                                {
                                    loginfo.Read();
                                    if (loginfo.NodeType == XmlNodeType.Text)
                                    {
                                        txtPassword.Text = loginfo.Value;
                                    }
                                }
                                done = true;
                                break;
                            }
                        }
                    }
                }
                loginfo.Close();
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
                    DataType.LoadHashTables(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\")) + "\\" + "DataTypesMapper.xml");
                    CurrentProject.DatabaseType = DatabaseTypes.SQLServer;
                    CurrentProject.Connect();
                    CurrentProject.ExtractorManager.DatabaseReaders.GetDatabases(CurrentProject);
                    //save the login information - Start
                    string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    XmlTextWriter loginfo = new XmlTextWriter(path+"\\logInfo.xml", null);
                    loginfo.WriteStartDocument();
                    loginfo.WriteStartElement("LoginInfo");
                    loginfo.WriteElementString("server", cmbServer.Text);
                    loginfo.WriteElementString("authentication", cmbAuthentication.SelectedIndex.ToString());
                    loginfo.WriteElementString("username", txtUserName.Text);
                    loginfo.WriteElementString("password", txtPassword.Text);            
                    loginfo.WriteEndElement();
                    loginfo.WriteEndDocument();
                    loginfo.Flush();
                    loginfo.Close();
                    //End

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            allowClose = true;
            this.Close();
        }

        #region Validation
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
        #endregion

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!allowClose)
                e.Cancel = true;
        }

    }
}
