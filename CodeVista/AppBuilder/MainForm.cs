using Common.Entities.MetaDataSchema;
using Common.Generation;
using DevExpress.XtraSplashScreen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AppBuilder
{
    public partial class MainForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public SplashScreenManager SplashManager
        {
            get { return splashScreenManager2; }
        }
        public MainForm()
        {
            InitializeComponent();
        }
        Common.Entities.MetaDataSchema.Project project = new Common.Entities.MetaDataSchema.Project();

        private void bBIConnection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Connection.Connect frm = new Connection.Connect();
            frm.CurrentProject = project;
            if (frm.ShowDialog() == DialogResult.Yes)
            {
                schemaViewer1.Project = project;
                schemaViewer1.BuildDatabases();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //System.IO.DirectoryInfo infoSave = new System.IO.DirectoryInfo(@"C:\IComputer\ISuperWork\Work\Personal Projects\CodeVista\CodeVista\bin\Debug\PamsNew\Save");
            //Database selectedDb = schemaViewer1.SelectedDatabase;
            //Database testDB = selectedDb.Clone() as Database;
            //Generator g = new Generator(CodeType.CSharp);

            //foreach (Table table in selectedDb.Tables)
            //{
            //    if (table.EntitySelected)
            //    {
            //        foreach (System.IO.FileInfo info in infoSave.GetFiles())
            //        {

            //            if (info.Name.Contains(table.Name))
            //            {
            //                info.CopyTo(txtTargetLocationSave.Text + @"\" + info.Name);
            //            }
            //            //Database db = schemaViewer1.Project.GetDatabaseByName("ShotecALL");
            //        }
            //    }
            //}

            //System.IO.DirectoryInfo infoView = new System.IO.DirectoryInfo(@"C:\IComputer\ISuperWork\Work\Personal Projects\CodeVista\CodeVista\bin\Debug\PamsNew\View");
            //foreach (Table table in selectedDb.Tables)
            //{
            //    if (table.EntitySelected)
            //    {
            //        foreach (System.IO.FileInfo info in infoView.GetFiles())
            //        {

            //            if (info.Name.Contains(table.Name))
            //            {
            //                info.CopyTo(txtTargetLocationSave.Text + @"\" + info.Name);
            //            }
            //            //Database db = schemaViewer1.Project.GetDatabaseByName("ShotecALL");
            //        }
            //    }
            //}
        }
    }
}
