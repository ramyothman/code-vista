using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Linq;
using System.Linq.Expressions;
namespace CodeVista.Controls
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

        #region Methods
        
        #region Tree Generating Functions
        public void BuildDatabases()
        {
            if (Project != null)
            {
                schemaTree.BeginUpdate();
                foreach (Common.Entities.MetaDataSchema.Database database in Project.Databases)
                {
                    //schemaTree.AppendNode(new object[]{database.Name,GetTypeString(Type.Database),null,0,0,0},null);
                    TreeNode node = new TreeNode(database.Name,0,0);
                    node.Tag = GetTypeString(SectionType.Database) + "," + database.Id;
                    schemaTree.Nodes.Add(node);

                    //schemaTree.AppendNode(new object[] { database.Name, GetTypeString(Type.Database) }, null);
                    TreeNode cnode = new TreeNode("NoType");
                    cnode.Tag = GetTypeString(SectionType.NoType);
                    node.Nodes.Add(cnode);
                    //schemaTree.AppendNode(new object[] { "NoType", GetTypeString(Type.NoType) }, node.Id, 1, 1, 0);
                }
                schemaTree.EndUpdate();
                
            }
        }
        #endregion

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
            SectionType str =  SectionType.NoType;
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
                    str = SectionType.StoredProcedure ;
                    break;
                case "Function":
                    str =  SectionType.Function;
                    break;
                    str = SectionType.View;
                    break;
                case "NoType":
                    str = SectionType.NoType;
                    break;
            }
            return str;
        }
        
        private void StartLoading()
        {
            frmLoading = new Loading();
            frmLoading.ShowDialog();
        }

        private string GetColumnTypeString(string text)
        {
            string []result = text.Split(',');
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

        private void MarkSelected()
        {
            
            Common.Entities.MetaDataSchema.Database db = null;
            bool dbselected = false;
            foreach (TreeNode n in schemaTree.Nodes)
            {
                if (n.Checked)
                {
                    dbselected = true;
                    //break;
                }
                else
                {
                    if (IsSelectedDatabase(n))
                    {
                        dbselected = true;
                        //break;
                    }
                }
                if (dbselected)
                {
                    string id = GetColumnIdString(n.Tag.ToString());
                    //Common.Entities.Entity.GetbyId(id, Project.Databases);
                    db = Common.Entities.Entity.GetbyId(id, Project.Databases);
                    db.UnSelect();
                    db.Select();
                    MarkChildSelected(n,db);
                    break;
                }
            }
            
        }
        private void MarkChildSelected(TreeNode childNodes, Common.Entities.Entity parentEntity)
        {
            foreach (TreeNode n in childNodes.Nodes)
            {
                string id = GetColumnIdString(n.Tag.ToString());
                string strType = GetColumnTypeString(n.Tag.ToString());
                SectionType selType = GetStringType(strType);
                Common.Entities.MetaDataSchema.Database database = null;
                switch (selType)
                {
                    case SectionType.Folder:
                        MarkChildSelected(n, parentEntity);
                        break;
                    case SectionType.Column:
                        Common.Entities.MetaDataSchema.Column col = null;
                        if (parentEntity is Common.Entities.MetaDataSchema.Table)
                        {
                            Common.Entities.MetaDataSchema.Table table = parentEntity as Common.Entities.MetaDataSchema.Table;
                            col = Common.Entities.Entity.GetbyId(id, table.Columns) as Common.Entities.MetaDataSchema.Column;
                            if (n.Checked)
                                col.Select();
                        }
                        else if (parentEntity is Common.Entities.MetaDataSchema.View)
                        {
                            Common.Entities.MetaDataSchema.View view = parentEntity as Common.Entities.MetaDataSchema.View;
                            col = Common.Entities.Entity.GetbyId(id, view.Columns) as Common.Entities.MetaDataSchema.Column;
                            if (n.Checked)
                                col.Select();
                        }
                        else if (parentEntity is Common.Entities.MetaDataSchema.Function)
                        {
                            Common.Entities.MetaDataSchema.Function function = parentEntity as Common.Entities.MetaDataSchema.Function;
                            col = Common.Entities.Entity.GetbyId(id, function.Parameters) as Common.Entities.MetaDataSchema.Column;
                            if (n.Checked)
                                col.Select();
                        }
                        break;
                    case SectionType.Function:
                        database = parentEntity as Common.Entities.MetaDataSchema.Database;
                        Common.Entities.MetaDataSchema.Function fn = Common.Entities.Entity.GetbyId(id, database.Functions) as Common.Entities.MetaDataSchema.Function;
                        if(n.Checked)
                            fn.Select();
                        MarkChildSelected(n, fn);
                        break;
                    case SectionType.StoredProcedure:
                        database = parentEntity as Common.Entities.MetaDataSchema.Database;
                        Common.Entities.MetaDataSchema.StoredProcedure sp = Common.Entities.Entity.GetbyId(id, database.StoredProcedures) as Common.Entities.MetaDataSchema.StoredProcedure;
                        if (n.Checked)
                            sp.Select();
                        MarkChildSelected(n, sp);
                        break;
                    case SectionType.Table:
                        database = parentEntity as Common.Entities.MetaDataSchema.Database;
                        Common.Entities.MetaDataSchema.Table tbl = Common.Entities.Entity.GetbyId(id, database.Tables) as Common.Entities.MetaDataSchema.Table;
                        if (n.Checked)
                            tbl.Select();
                        MarkChildSelected(n, tbl);
                        break;
                    case SectionType.View:
                        database = parentEntity as Common.Entities.MetaDataSchema.Database;
                        Common.Entities.MetaDataSchema.View vw = Common.Entities.Entity.GetbyId(id, database.Views) as Common.Entities.MetaDataSchema.View;
                        if (n.Checked)
                            vw.Select();
                        MarkChildSelected(n, vw);
                        break;
                    
                }
                
            }
        }
        #endregion


        #region Properties
        Loading frmLoading = null;
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

        public Common.Entities.MetaDataSchema.Database SelectedDatabase
        {
            get
            {
                Common.Entities.MetaDataSchema.Database db = null;
                TreeNode selectedNode = null;
                foreach (TreeNode n in schemaTree.Nodes)
                {
                    if (n.Checked)
                    {
                        selectedNode = n;
                        break;
                    }
                    else
                    {
                        if (IsSelectedDatabase(n))
                        {
                            selectedNode = n;
                            break;
                        }
                    }
                }
                if (selectedNode != null)
                {
                    string id = GetColumnIdString(selectedNode.Tag.ToString());
                    //Common.Entities.Entity.GetbyId(id, Project.Databases);
                    db = Common.Entities.Entity.GetbyId(id, Project.Databases) ;
                    MarkSelected();
                    return db;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<string> SelectedTables
        {
            get
            {
                List<string> _selectedTables = new List<string>();
                
                TreeNode selectedNode = null;
                
                foreach (TreeNode n in schemaTree.Nodes)
                {
                    if (n.Checked)
                    {
                        foreach (TreeNode f in n.Nodes)
                        {
                            foreach (TreeNode t in f.Nodes)
                            {
                                if(t.Checked)
                                {
                                    selectedNode = t;
                                    string id = GetColumnIdString(selectedNode.Tag.ToString());
                                    Common.Entities.MetaDataSchema.Table table = Common.Entities.Entity.GetbyId(id, SelectedDatabase.Tables);
                                    if (table != null)
                                    {
                                        _selectedTables.Add(table.Name);
                                    }
                                }
                            }
                        }
                    }
                }
                return _selectedTables;
            }
        }

        public bool IsSelectedDatabase(TreeNode node)
        {
            foreach (TreeNode n in node.Nodes)
            {
                if (n.Checked)
                {
                    return true;
                }
                else
                {
                    bool isSelected = IsSelectedDatabase(n);
                    if (isSelected)
                        return true;
                }
            }
            return false;
        }
        #endregion

        #region Custom Event Handlers
        public void Project_StartLoading(object sender, Common.Events.LoadingEventArgs e)
        {
            
            frmLoading.SetText("Start Reading From " + e.ParentData + " " + e.LoadedData);
        }

        public void Project_EndLoading(object sender, Common.Events.LoadingEventArgs e)
        {
            frmLoading.SetText("Schema Read From " + e.ParentData + " " + e.LoadedData);
            frmLoading.IncrementProgress();
        }

        private void bgGetSchema_DoWork(object sender, DoWorkEventArgs e)
        {
            Project.ExtractorManager.DatabaseReaders.GetTables(Project);
            foreach (Common.Entities.MetaDataSchema.Database db in Project.Databases)
            {
                Project.ExtractorManager.DatabaseReaders.GetColumns(db);
            }
        }

        private void schemaTree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            string value = GetColumnTypeString(e.Node.Nodes[0].Tag.ToString());
            if (value == GetTypeString(SectionType.NoType))
            {

                e.Node.Nodes.Clear();
                string nodeName = e.Node.Text.ToString();
                string nodeType = GetColumnTypeString(e.Node.Tag.ToString());
                if (GetTypeString(SectionType.Database) == nodeType)
                {
                    Thread thread = new Thread(new ThreadStart(StartLoading));
                    thread.Start();

                    Common.Entities.MetaDataSchema.Database db = Common.Entities.MetaDataSchema.Database.GetByName(Project.Databases, nodeName);
                    schemaTree.BeginUpdate();
                    e.Node.Text = nodeName + "(expanding...)";
                    if (db != null)
                    {
                        #region Table Loading
                        #region Extrating Table Information
                        if (db.Tables.Count == 0)
                        {
                            Project.ExtractorManager.DatabaseReaders.GetTables(db);
                            while (frmLoading == null)
                            {

                            }
                            frmLoading.SetText(db.Tables.Count + " Tables Loaded");
                            frmLoading.LoadProgress(db.Tables.Count);
                            foreach (Common.Entities.MetaDataSchema.Table tbl in db.Tables)
                            {
                                Project.ExtractorManager.DatabaseReaders.GetColumns(tbl);
                            }
                        }
                        #endregion

                        #region Adding Table Information to the Tree
                        TreeNode FolderNode = new TreeNode("Tables", 6, 6);
                        FolderNode.Tag = GetTypeString(SectionType.Folder);
                        e.Node.Nodes.Add(FolderNode);
                        var tables = db.Tables.OrderBy(x => x.Schema).ThenBy(n => n.Name).ToList();
                        foreach (Common.Entities.MetaDataSchema.Table tb in tables)
                        {

                            TreeNode tnode = new TreeNode(tb.Schema +"."+ tb.Name, 1, 1);
                            tnode.Tag = GetTypeString(SectionType.Table) + "," + tb.Id;
                            FolderNode.Nodes.Add(tnode);
                            // GetTypeString(Type.Table) }, e.Node.Id, 1, 1, 0);
                            foreach (Common.Entities.MetaDataSchema.Column col in tb.Columns)
                            {
                                int imageIndex = 2;
                                if (col.IsPrimary)
                                    imageIndex = 3;
                                else if (col.IsForeign)
                                    imageIndex = 4;
                                else if (col.IsComputed)
                                    imageIndex = 5;
                                TreeNode cnode = new TreeNode(col.Name, imageIndex, imageIndex);
                                cnode.Tag = GetTypeString(SectionType.Column) + "," + col.Id;
                                tnode.Nodes.Add(cnode);
                                //schemaTree.AppendNode(new object[] { col.Name, GetTypeString(Type.Column) }, tnode.Id, 2, 2, 0);
                            }
                        }
                        #endregion
                        #endregion

                        #region View Loading
                        #region Extrating Views Information
                        if (db.Views.Count == 0)
                        {
                            Project.ExtractorManager.DatabaseReaders.GetViews(db);
                            while (frmLoading == null)
                            {

                            }
                            frmLoading.LoadProgress(db.Views.Count);
                            frmLoading.SetText(db.Views.Count + " Views Loaded");
                            foreach (Common.Entities.MetaDataSchema.View tbl in db.Views)
                            {
                                Project.ExtractorManager.DatabaseReaders.GetColumns(tbl);
                            }
                        }
                        #endregion

                        #region Adding Views Information to the Tree
                        FolderNode = new TreeNode("Views", 6, 6);
                        FolderNode.Tag = GetTypeString(SectionType.Folder);
                        e.Node.Nodes.Add(FolderNode);
                        foreach (Common.Entities.MetaDataSchema.View tb in db.Views)
                        {

                            TreeNode tnode = new TreeNode(tb.Name, 7, 7);
                            tnode.Tag = GetTypeString(SectionType.View) + "," + tb.Id;
                            FolderNode.Nodes.Add(tnode);
                            // GetTypeString(Type.Table) }, e.Node.Id, 1, 1, 0);
                            foreach (Common.Entities.MetaDataSchema.Column col in tb.Columns)
                            {
                                int imageIndex = 2;
                                if (col.IsPrimary)
                                    imageIndex = 3;
                                else if (col.IsForeign)
                                    imageIndex = 4;
                                else if (col.IsComputed)
                                    imageIndex = 5;
                                TreeNode cnode = new TreeNode(col.Name, imageIndex, imageIndex);
                                cnode.Tag = GetTypeString(SectionType.Column) + "," + col.Id;
                                tnode.Nodes.Add(cnode);
                                //schemaTree.AppendNode(new object[] { col.Name, GetTypeString(Type.Column) }, tnode.Id, 2, 2, 0);
                            }

                        }
                        #endregion
                        #endregion

                        #region Function Loading
                        #region Extracting Function Information
                        if (db.Functions.Count == 0)
                        {
                            Project.ExtractorManager.DatabaseReaders.GetFunctions(db);
                            while (frmLoading == null)
                            {

                            }
                            frmLoading.LoadProgress(db.Functions.Count);
                            frmLoading.SetText(db.Functions.Count + " Functions Loaded");
                            foreach (Common.Entities.MetaDataSchema.Function fn in db.Functions)
                            {
                                if(!string.IsNullOrEmpty(fn.FunctionId))
                                    Project.ExtractorManager.DatabaseReaders.GetFunctionParameters(fn);
                            }
                        }
                        #endregion

                        #region Adding Function Information to the Tree
                        FolderNode = new TreeNode("Functions", 6, 6);
                        FolderNode.Tag = GetTypeString(SectionType.Folder);
                        e.Node.Nodes.Add(FolderNode);
                        foreach (Common.Entities.MetaDataSchema.Function tb in db.Functions)
                        {

                            TreeNode tnode = new TreeNode(tb.Name, 8, 8);
                            tnode.Tag = GetTypeString(SectionType.Function) + "," + tb.Id;
                            FolderNode.Nodes.Add(tnode);

                            TreeNode ParameterNode = new TreeNode("Parameters", 6, 6);
                            ParameterNode.Tag = GetTypeString(SectionType.Folder);
                            tnode.Nodes.Add(ParameterNode);
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
                                TreeNode cnode = new TreeNode(col.Name, imageIndex, imageIndex);
                                cnode.Tag = GetTypeString(SectionType.Column) + "," + col.Id;
                                if (col.ColumnDirection == ColumnDirection.Output)
                                {
                                    cnode.Text = "Returns " + col.ColumnDataType.SQLType;
                                }
                                ParameterNode.Nodes.Add(cnode);
                            }

                        }
                        #endregion
                        #endregion

                        #region Stored Procedure Loading
                        #region Extracting Stored Procedure Information
                        if (db.StoredProcedures.Count == 0)
                        {
                            Project.ExtractorManager.DatabaseReaders.GetStoredProcedures(db);
                            while (frmLoading == null)
                            {

                            }
                            frmLoading.LoadProgress(db.StoredProcedures.Count);
                            frmLoading.SetText(db.StoredProcedures.Count + " Stored Procedures Loaded");
                            foreach (Common.Entities.MetaDataSchema.StoredProcedure sp in db.StoredProcedures)
                            {
                                Project.ExtractorManager.DatabaseReaders.GetStoredProcedureParameters(sp);
                            }
                        }
                        #endregion

                        #region Adding Stored Procedure to the Tree
                        FolderNode = new TreeNode("StoredProcedure", 6, 6);
                        FolderNode.Tag = GetTypeString(SectionType.Folder);
                        e.Node.Nodes.Add(FolderNode);
                        foreach (Common.Entities.MetaDataSchema.StoredProcedure tb in db.StoredProcedures)
                        {

                            TreeNode tnode = new TreeNode(tb.Name, 11, 11);
                            tnode.Tag = GetTypeString(SectionType.StoredProcedure) + "," + tb.Id;
                            FolderNode.Nodes.Add(tnode);

                            TreeNode ParameterNode = new TreeNode("Parameters", 6, 6);
                            ParameterNode.Tag = GetTypeString(SectionType.Folder);
                            tnode.Nodes.Add(ParameterNode);
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
                                TreeNode cnode = new TreeNode(col.Name, imageIndex, imageIndex);
                                cnode.Tag = GetTypeString(SectionType.Column) + "," + col.Id;
                                if (col.ColumnDirection == ColumnDirection.Output)
                                {
                                    cnode.Text = "Returns " + col.ColumnDataType.SQLType;
                                }
                                ParameterNode.Nodes.Add(cnode);
                            }

                        }
                        #endregion
                        #endregion

                    }
                    e.Node.Text = nodeName;
                    schemaTree.EndUpdate();
                    thread.Abort();
                }
            }
        }

        private void schemaTree_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            AnotherDBSelected(e.Node,!e.Node.Checked);
        }

        private void schemaTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            SectionType type = GetStringType(GetColumnTypeString(e.Node.Tag.ToString()));
            switch (type)
            {
                case SectionType.Database:
                case SectionType.Function:
                case SectionType.StoredProcedure:
                case SectionType.View:
                case SectionType.Table:
                case SectionType.Folder:
                    
                    foreach (TreeNode node in e.Node.Nodes)
                    {
                        node.Checked = e.Node.Checked;
                    }
                    break;
            }
        }

        private void AnotherDBSelected(TreeNode node,bool checkedState)
        {
            if (node == null)
                return;
            SectionType type = GetStringType(GetColumnTypeString(node.Tag.ToString()));
            string id = GetColumnIdString(node.Tag.ToString());
            if (type == SectionType.Database)
            {
                foreach (TreeNode n in schemaTree.Nodes)
                {
                    if (GetColumnIdString(n.Tag.ToString()) != id)
                    {
                        if(n.Checked)
                            n.Checked = !checkedState;
                    }
                }
            }
            else
            {
                AnotherDBSelected(node.Parent,checkedState);
            }
        }
        #endregion

    }
}
