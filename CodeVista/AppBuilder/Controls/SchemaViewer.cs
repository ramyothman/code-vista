using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace AppBuilder.Controls
{

    public enum SectionType
    {
        Database = 0,
        Table = 1,
        Column = 2,
        Folder = 3,
        View = 4,
        NoType = 5,
        Function = 6,
        StoredProcedure = 7

    }
    public partial class SchemaViewer : UserControl
    {
        #region Constructors
        public SchemaViewer()
        {
            InitializeComponent();
        }
        #endregion

        #region Declarations & Properties
        
        private Common.Entities.MetaDataSchema.Project _project = null;
        public Common.Entities.MetaDataSchema.Project Project
        {
            get
            {
                return _project;
            }
            set
            {
                _project = value;
                if (value != null)
                {
                    _project.ExtractorManager.DatabaseReaders.StartLoading += new Common.MetaDataExtraction.DatabaseExtractors.DatabaseExtractor.StartLoadingEventHandler(Project_StartLoading);
                    _project.ExtractorManager.DatabaseReaders.EndLoading += new Common.MetaDataExtraction.DatabaseExtractors.DatabaseExtractor.EndLoadingEventHandler(Project_EndLoading);
                }
                else
                {
                    schemaTree.Nodes.Clear();
                }
            }
        }

        
        #endregion

        #region Helper Methods
        private string GetTypeString(SectionType type)
        {
            string str = "";
            switch (type)
            {
                case SectionType.Column:
                    str = "Column";
                    break;
                case SectionType.Database:
                    str = "Database";
                    break;
                case SectionType.Folder:
                    str = "Folder";
                    break;
                case SectionType.Table:
                    str = "Table";
                    break;
                case SectionType.View:
                    str = "View";
                    break;
                case SectionType.StoredProcedure:
                    str = "Stored Procedure";
                    break;
                case SectionType.Function:
                    str = "Function";
                    break;
                case SectionType.NoType:
                    str = "NoType";
                    break;
            }
            return str;
        }
        private SectionType GetStringType(string type)
        {
            SectionType str = SectionType.NoType;
            switch (type)
            {
                case "Column":
                    str = SectionType.Column;
                    break;
                case "Database":
                    str = SectionType.Database;
                    break;
                case "Table":
                    str = SectionType.Table;
                    break;
                case "Folder":
                    str = SectionType.Folder;
                    break;
                case "View":
                case "Stored Procedure":
                    str = SectionType.StoredProcedure;
                    break;
                case "Function":
                    str = SectionType.Function;
                    break;
                    str = SectionType.View;
                    break;
                case "NoType":
                    str = SectionType.NoType;
                    break;
            }
            return str;
        }

        private string GetColumnTypeString(string text)
        {
            string[] result = text.Split(',');
            if (result.Length > 0)
            {
                return result[0];
            }
            return "";
        }
        private string GetColumnIdString(string text)
        {
            string[] result = text.Split(',');
            if (result.Length > 1)
            {
                return result[1];
            }
            return "";
        }

        #endregion

        #region Custom Events
        public void Project_StartLoading(object sender, Common.Events.LoadingEventArgs e)
        {
            Tools.MainForm.SplashManager.SetWaitFormDescription(String.Format("Loading {0} {1}", e.ParentData, e.LoadedData));
        }

        public void Project_EndLoading(object sender, Common.Events.LoadingEventArgs e)
        {
            //Tools.MainForm.SplashManager.SetWaitFormDescription(String.Format("Loading {0} {1}", e.ParentData, e.LoadedData));
            //Tools.MainForm.SplashManager.IncrementProgress();
        }
        #endregion

        #region Schema Operations

        #region Node Addition Operation
        private Datasets.MainDS.SchemaRow AddDatabaseNode(Guid id, string schema, string name, string fullName)
        {
            return AddNodeRow(id, null, schema, name, fullName, GetTypeString(SectionType.Database), "", 0);
        }

        private Datasets.MainDS.SchemaRow AddTableNode(Guid id, Guid parent, string schema, string name, string fullName)
        {
            return AddNodeRow(id, parent, schema, name, fullName, GetTypeString(SectionType.Table), "", 1);
        }

        private Datasets.MainDS.SchemaRow AddColumnNode(Guid id, Guid parent, string fullName, string dataType,int imageIndex)
        {
            return AddNodeRow(id, parent, "", fullName, fullName, GetTypeString(SectionType.Column), dataType, imageIndex);
        }

        private Datasets.MainDS.SchemaRow AddFolderNode(Guid id, Guid parent, string fullName)
        {
            return AddNodeRow(id, parent, "", fullName, fullName, GetTypeString(SectionType.Folder), "", 6);
        }

        private Datasets.MainDS.SchemaRow AddViewNode(Guid id, Guid parent,string schema, string name, string fullName)
        {
            return AddNodeRow(id, parent, schema, name, fullName, GetTypeString(SectionType.View), "", 7);
        }

        private Datasets.MainDS.SchemaRow AddStoredProcedureNode(Guid id, Guid parent, string schema, string name, string fullName)
        {
            return AddNodeRow(id, parent, schema, name, fullName, GetTypeString(SectionType.StoredProcedure), "", 11);
        }

        private Datasets.MainDS.SchemaRow AddFunctionNode(Guid id, Guid parent, string schema, string name, string fullName)
        {
            return AddNodeRow(id, parent, schema, name, fullName, GetTypeString(SectionType.Function), "", 8);
        }

        private Datasets.MainDS.SchemaRow AddNodeRow(Guid id, Guid? parent, string schema, string name, string fullName, string schemaType, string dataType, int imageIndex)
        {
            Datasets.MainDS.SchemaRow row = mainDS.Schema.NewSchemaRow();
            row.Name = name;
            row.SchemaName = schema;
            row.FullName = fullName;
            row.ID = id;
            row.SchemaType = schemaType;
            row.ImageIndex = imageIndex;
            if(parent.HasValue) row.ParentID = parent.Value;
            row.DataType = dataType;
            mainDS.Schema.Rows.Add(row);
            return row;
        }
        #endregion

        public void BuildDatabases()
        {
            if (Project != null)
            {
                schemaTree.BeginUpdate();
                foreach (Common.Entities.MetaDataSchema.Database database in Project.Databases)
                {
                    Datasets.MainDS.SchemaRow row = mainDS.Schema.NewSchemaRow();
                    row.Name = database.Name;
                    row.SchemaName = database.ParentProject.ServerName;
                    row.FullName = database.Name;
                    row.ID = database.GuidId;
                    row.SchemaType = "Database";
                    row.ImageIndex = 0;
                    mainDS.Schema.Rows.Add(row);

                    Datasets.MainDS.SchemaRow rowTemp = mainDS.Schema.NewSchemaRow();
                    rowTemp.Name = "NoType";
                    rowTemp.SchemaName = "";
                    rowTemp.FullName = "NoType";
                    rowTemp.ID = Guid.NewGuid();
                    rowTemp.ParentID = database.GuidId;
                    rowTemp.SchemaType = "NoType";
                    rowTemp.ImageIndex = 1;
                    mainDS.Schema.Rows.Add(rowTemp);

                    //schemaTree.AppendNode(new object[]{database.Name,GetTypeString(Type.Database),null,0,0,0},null);
                    //TreeNode node = new TreeNode(database.Name, 0, 0);
                    //node.Tag = String.Format("{0},{1}", GetTypeString(SectionType.Database), database.Id);
                    //schemaTree.Nodes.Add(node);

                    //schemaTree.AppendNode(new object[] { database.Name, GetTypeString(Type.Database) }, null);
                    //TreeNode cnode = new TreeNode("NoType");
                    //cnode.Tag = GetTypeString(SectionType.NoType);
                    //node.Nodes.Add(cnode);
                    //schemaTree.AppendNode(new object[] { "NoType", GetTypeString(Type.NoType) }, node.Id, 1, 1, 0);
                }
                schemaTree.EndUpdate();
                //schemaTree.DataSource = mainDS.Schema;

            }
        }
        #endregion

        private void schemaTree_BeforeExpand(object sender, DevExpress.XtraTreeList.BeforeExpandEventArgs e)
        {
            //Tools.MainForm.SplashManager.ShowWaitForm();
            var x = e.Node.GetValue(5);
            var row = mainDS.Schema.Select(String.Format("ID = '{0}'", x));
            if(row.Length == 0)
            {
                return;
                //row[0].Delete();

            }
            string value = GetColumnTypeString(e.Node.Nodes[0].GetValue(1).ToString());
            if (value == GetTypeString(SectionType.NoType))
            {
                
                string nodeName = e.Node.GetValue(0).ToString();
                string nodeType = GetColumnTypeString(e.Node.GetValue(1).ToString());
                var rowNoType = mainDS.Schema.Select(String.Format("ParentID = '{0}' and SchemaType = 'NoType'", x));
                if(rowNoType.Length > 0)
                    rowNoType[0].Delete();
                if (GetTypeString(SectionType.Database) == nodeType)
                {
                    Tools.MainForm.SplashManager.ShowWaitForm();
                    
                    Common.Entities.MetaDataSchema.Database db = Common.Entities.MetaDataSchema.Database.GetByName(Project.Databases, nodeName);
                    schemaTree.BeginUpdate();
                    //e.Node.Text = nodeName + "(expanding...)";
                    if (db != null)
                    {
                        #region Table Loading
                        #region Extrating Table Information
                        if (db.Tables.Count == 0)
                        {
                            Project.ExtractorManager.DatabaseReaders.GetTables(db);

                            Tools.MainForm.SplashManager.SetWaitFormDescription(db.Tables.Count + " Tables Loaded");
                            foreach (Common.Entities.MetaDataSchema.Table tbl in db.Tables)
                            {
                                Project.ExtractorManager.DatabaseReaders.GetColumns(tbl);
                            }
                        }
                        #endregion

                        #region Adding Table Information to the Tree
                        Datasets.MainDS.SchemaRow tableFolder = AddFolderNode(Guid.NewGuid(), ((Datasets.MainDS.SchemaRow)row[0]).ID, "Tables");
                        
                        foreach (Common.Entities.MetaDataSchema.Table tb in db.Tables)
                        {
                            Datasets.MainDS.SchemaRow tableRow = AddTableNode(tb.GuidId, tableFolder.ID, tb.Schema, tb.Name, tb.Schema + "." + tb.Name);

                            foreach (Common.Entities.MetaDataSchema.Column col in tb.Columns)
                            {
                                int imageIndex = 2;
                                if (col.IsPrimary)
                                    imageIndex = 3;
                                else if (col.IsForeign)
                                    imageIndex = 4;
                                else if (col.IsComputed)
                                    imageIndex = 5;
                                Datasets.MainDS.SchemaRow colRow = AddColumnNode(col.GuidId, tableRow.ID, col.Name, col.ColumnDataType.DotNetType, imageIndex);
                            }
                        }
                        #endregion
                        #endregion

                        #region View Loading
                        #region Extrating Views Information
                        if (db.Views.Count == 0)
                        {
                            Project.ExtractorManager.DatabaseReaders.GetViews(db);
                            Tools.MainForm.SplashManager.SetWaitFormDescription(db.Views.Count + " Views Loaded");
                            foreach (Common.Entities.MetaDataSchema.View tbl in db.Views)
                            {
                                Project.ExtractorManager.DatabaseReaders.GetColumns(tbl);
                            }
                        }
                        #endregion

                        #region Adding Views Information to the Tree
                        Datasets.MainDS.SchemaRow viewFolder = AddFolderNode(Guid.NewGuid(), ((Datasets.MainDS.SchemaRow)row[0]).ID, "Views");
                        
                        foreach (Common.Entities.MetaDataSchema.View tb in db.Views)
                        {
                            Datasets.MainDS.SchemaRow viewRow = AddViewNode(tb.GuidId, viewFolder.ID, tb.Schema, tb.Name, tb.Schema + "." + tb.Name);
                            
                            foreach (Common.Entities.MetaDataSchema.Column col in tb.Columns)
                            {
                                int imageIndex = 2;
                                if (col.IsPrimary)
                                    imageIndex = 3;
                                else if (col.IsForeign)
                                    imageIndex = 4;
                                else if (col.IsComputed)
                                    imageIndex = 5;
                                Datasets.MainDS.SchemaRow colRow = AddColumnNode(col.GuidId, viewRow.ID, col.Name, col.ColumnDataType.Name, imageIndex);
                                
                            }

                        }
                        #endregion
                        #endregion

                        #region Function Loading
                        #region Extracting Function Information
                        if (db.Functions.Count == 0)
                        {
                            Project.ExtractorManager.DatabaseReaders.GetFunctions(db);
                            Tools.MainForm.SplashManager.SetWaitFormDescription(db.Functions.Count + " Functions Loaded");
                            foreach (Common.Entities.MetaDataSchema.Function fn in db.Functions)
                            {
                                if (!string.IsNullOrEmpty(fn.FunctionId))
                                    Project.ExtractorManager.DatabaseReaders.GetFunctionParameters(fn);
                            }
                        }
                        #endregion

                        #region Adding Function Information to the Tree
                        Datasets.MainDS.SchemaRow functionFolder = AddFolderNode(Guid.NewGuid(), ((Datasets.MainDS.SchemaRow)row[0]).ID, "Functions");

                        foreach (Common.Entities.MetaDataSchema.Function tb in db.Functions)
                        {
                            Datasets.MainDS.SchemaRow functionRow = AddFunctionNode(tb.GuidId, functionFolder.ID, tb.Schema, tb.Name, tb.Schema + "." + tb.Name);
                            Datasets.MainDS.SchemaRow parametersFolder = AddFolderNode(Guid.NewGuid(), functionRow.ID, "Parameters");
                            
                            foreach (Common.Entities.MetaDataSchema.Column col in tb.Parameters)
                            {
                                int imageIndex = 9;
                                if (col.IsPrimary)
                                    imageIndex = 3;
                                else if (col.IsForeign)
                                    imageIndex = 4;
                                else if (col.IsComputed)
                                    imageIndex = 5;
                                if (col.ColumnDirection == ColumnDirection.Output)
                                    imageIndex = 10;

                                if (col.ColumnDirection == ColumnDirection.Output)
                                {
                                    Datasets.MainDS.SchemaRow colRow = AddColumnNode(col.GuidId, parametersFolder.ID, "return " + col.Name, col.ColumnDataType.Name, imageIndex);
                                }
                                else
                                {
                                    Datasets.MainDS.SchemaRow colRow = AddColumnNode(col.GuidId, parametersFolder.ID, col.Name, col.ColumnDataType.Name, imageIndex);
                                }
                            }
                        }
                        #endregion
                        #endregion

                        #region Stored Procedure Loading
                        #region Extracting Stored Procedure Information
                        if (db.StoredProcedures.Count == 0)
                        {
                            Project.ExtractorManager.DatabaseReaders.GetStoredProcedures(db);
                            Tools.MainForm.SplashManager.SetWaitFormDescription(db.StoredProcedures.Count + " Stored Procedures Loaded");
                            foreach (Common.Entities.MetaDataSchema.StoredProcedure sp in db.StoredProcedures)
                            {
                                Project.ExtractorManager.DatabaseReaders.GetStoredProcedureParameters(sp);
                            }
                        }
                        #endregion

                        #region Adding Stored Procedure to the Tree
                        Datasets.MainDS.SchemaRow procFolder = AddFolderNode(Guid.NewGuid(), ((Datasets.MainDS.SchemaRow)row[0]).ID, "Stored Procedures");
                        
                        foreach (Common.Entities.MetaDataSchema.StoredProcedure tb in db.StoredProcedures)
                        {
                            Datasets.MainDS.SchemaRow procRow = AddStoredProcedureNode(tb.GuidId, procFolder.ID, tb.Schema, tb.Name, tb.Schema + "." + tb.Name);
                            Datasets.MainDS.SchemaRow parametersFolder = AddFolderNode(Guid.NewGuid(), procRow.ID, "Parameters");
                            
                            // GetTypeString(Type.Table) }, e.Node.Id, 1, 1, 0);
                            foreach (Common.Entities.MetaDataSchema.Column col in tb.Parameters)
                            {
                                int imageIndex = 9;
                                if (col.IsPrimary)
                                    imageIndex = 3;
                                else if (col.IsForeign)
                                    imageIndex = 4;
                                else if (col.IsComputed)
                                    imageIndex = 5;
                                if (col.ColumnDirection == ColumnDirection.Output)
                                    imageIndex = 10;
                                
                                if (col.ColumnDirection == ColumnDirection.Output)
                                {
                                    Datasets.MainDS.SchemaRow colRow = AddColumnNode(col.GuidId, parametersFolder.ID, "return " + col.Name, col.ColumnDataType.Name, imageIndex);
                                }
                                else
                                {
                                    Datasets.MainDS.SchemaRow colRow = AddColumnNode(col.GuidId, parametersFolder.ID, col.Name, col.ColumnDataType.Name, imageIndex);
                                }
                            }

                        }
                        #endregion
                        #endregion

                    }
                    schemaTree.EndUpdate();
                    Tools.MainForm.SplashManager.CloseWaitForm();
                }

            }
        }

        private void LoadSchemaDetails()
        {

        }
    }
}
