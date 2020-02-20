using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CodeVista
{
    public partial class MainForm : Form
    {
        Common.Entities.MetaDataSchema.Project project = new Common.Entities.MetaDataSchema.Project();
        SchemaViewerForm SchemaViewer = new SchemaViewerForm();
        ActionStatusForm ActionStatus = new ActionStatusForm();
        public MainForm()
        {
            InitializeComponent();
            mainDockPanel.Parent = this;
            CreateBasicLayout();
        }
        private void CreateBasicLayout()
        {
            SchemaViewer.Show(mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.DockLeft);
            ActionStatus.Show(mainDockPanel, WeifenLuo.WinFormsUI.Docking.DockState.DockBottomAutoHide);
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void connectObjectExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm frm = new LoginForm();
            frm.CurrentProject = project;
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                SchemaViewer.mainSchemaViewer.Project = project;
                SchemaViewer.mainSchemaViewer.BuildDatabases();
            }
        }

        private void disconnectObjectExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SchemaViewer.mainSchemaViewer.Project = null;
            project = new Common.Entities.MetaDataSchema.Project();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Common.SourceCodeExtraction.SourceExtractorManager mngr = new Common.SourceCodeExtraction.SourceExtractorManager();
            mngr.LoadSourceProject(@"I:\Work\External Projects\WebProjects\WebProjects\Appraisers\Appraisers.sln");
        }
    }
}
