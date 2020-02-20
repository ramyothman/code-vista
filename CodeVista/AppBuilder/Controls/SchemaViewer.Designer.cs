namespace AppBuilder.Controls
{
    partial class SchemaViewer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SchemaViewer));
            this.schemaTree = new DevExpress.XtraTreeList.TreeList();
            this.colFullName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colSchemaType = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colDataColumnID = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.colSchemaName = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.treeListColumn1 = new DevExpress.XtraTreeList.Columns.TreeListColumn();
            this.mainDSBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mainDS = new AppBuilder.Datasets.MainDS();
            this.dbImageIcons = new System.Windows.Forms.ImageList(this.components);
            this.chkimage = new System.Windows.Forms.ImageList(this.components);
            this.bgGetSchema = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.schemaTree)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDSBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDS)).BeginInit();
            this.SuspendLayout();
            // 
            // schemaTree
            // 
            this.schemaTree.Columns.AddRange(new DevExpress.XtraTreeList.Columns.TreeListColumn[] {
            this.colFullName,
            this.colSchemaType,
            this.colDataColumnID,
            this.colName,
            this.colSchemaName,
            this.treeListColumn1});
            this.schemaTree.DataMember = "Schema";
            this.schemaTree.DataSource = this.mainDSBindingSource;
            this.schemaTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schemaTree.Location = new System.Drawing.Point(0, 0);
            this.schemaTree.Name = "schemaTree";
            this.schemaTree.OptionsBehavior.AllowRecursiveNodeChecking = true;
            this.schemaTree.OptionsBehavior.EnableFiltering = true;
            this.schemaTree.OptionsBehavior.PopulateServiceColumns = true;
            this.schemaTree.OptionsFilter.FilterMode = DevExpress.XtraTreeList.FilterMode.Smart;
            this.schemaTree.OptionsFind.AllowFindPanel = true;
            this.schemaTree.OptionsFind.AlwaysVisible = true;
            this.schemaTree.OptionsView.ShowCheckBoxes = true;
            this.schemaTree.SelectImageList = this.dbImageIcons;
            this.schemaTree.Size = new System.Drawing.Size(326, 557);
            this.schemaTree.StateImageList = this.chkimage;
            this.schemaTree.TabIndex = 0;
            this.schemaTree.BeforeExpand += new DevExpress.XtraTreeList.BeforeExpandEventHandler(this.schemaTree_BeforeExpand);
            // 
            // colFullName
            // 
            this.colFullName.FieldName = "FullName";
            this.colFullName.MinWidth = 67;
            this.colFullName.Name = "colFullName";
            this.colFullName.OptionsColumn.AllowEdit = false;
            this.colFullName.OptionsColumn.ReadOnly = true;
            this.colFullName.SortOrder = System.Windows.Forms.SortOrder.Ascending;
            this.colFullName.Visible = true;
            this.colFullName.VisibleIndex = 0;
            this.colFullName.Width = 440;
            // 
            // colSchemaType
            // 
            this.colSchemaType.FieldName = "SchemaType";
            this.colSchemaType.Name = "colSchemaType";
            this.colSchemaType.OptionsColumn.AllowEdit = false;
            this.colSchemaType.OptionsColumn.ReadOnly = true;
            this.colSchemaType.Width = 272;
            // 
            // colDataColumnID
            // 
            this.colDataColumnID.Caption = "Data Type";
            this.colDataColumnID.FieldName = "DataType";
            this.colDataColumnID.Name = "colDataColumnID";
            this.colDataColumnID.OptionsColumn.AllowEdit = false;
            this.colDataColumnID.OptionsColumn.ReadOnly = true;
            this.colDataColumnID.Visible = true;
            this.colDataColumnID.VisibleIndex = 1;
            this.colDataColumnID.Width = 245;
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.OptionsColumn.ReadOnly = true;
            this.colName.OptionsColumn.ShowInCustomizationForm = false;
            this.colName.Width = 61;
            // 
            // colSchemaName
            // 
            this.colSchemaName.FieldName = "SchemaName";
            this.colSchemaName.Name = "colSchemaName";
            this.colSchemaName.OptionsColumn.AllowEdit = false;
            this.colSchemaName.OptionsColumn.ReadOnly = true;
            this.colSchemaName.OptionsColumn.ShowInCustomizationForm = false;
            this.colSchemaName.Width = 62;
            // 
            // treeListColumn1
            // 
            this.treeListColumn1.Caption = "treeListColumn1";
            this.treeListColumn1.FieldName = "ID";
            this.treeListColumn1.Name = "treeListColumn1";
            this.treeListColumn1.OptionsColumn.ShowInCustomizationForm = false;
            // 
            // mainDSBindingSource
            // 
            this.mainDSBindingSource.DataSource = this.mainDS;
            this.mainDSBindingSource.Position = 0;
            // 
            // mainDS
            // 
            this.mainDS.DataSetName = "MainDS";
            this.mainDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dbImageIcons
            // 
            this.dbImageIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("dbImageIcons.ImageStream")));
            this.dbImageIcons.TransparentColor = System.Drawing.Color.Transparent;
            this.dbImageIcons.Images.SetKeyName(0, "database.png");
            this.dbImageIcons.Images.SetKeyName(1, "table.png");
            this.dbImageIcons.Images.SetKeyName(2, "column.png");
            this.dbImageIcons.Images.SetKeyName(3, "PrimaryKey.png");
            this.dbImageIcons.Images.SetKeyName(4, "foreignKey.png");
            this.dbImageIcons.Images.SetKeyName(5, "ComputedColumn.png");
            this.dbImageIcons.Images.SetKeyName(6, "folder.png");
            this.dbImageIcons.Images.SetKeyName(7, "Views.png");
            this.dbImageIcons.Images.SetKeyName(8, "Functions.png");
            this.dbImageIcons.Images.SetKeyName(9, "Parameters.png");
            this.dbImageIcons.Images.SetKeyName(10, "returnParameter.png");
            this.dbImageIcons.Images.SetKeyName(11, "StoredProcedures.jpg");
            // 
            // chkimage
            // 
            this.chkimage.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("chkimage.ImageStream")));
            this.chkimage.TransparentColor = System.Drawing.Color.Transparent;
            this.chkimage.Images.SetKeyName(0, "");
            this.chkimage.Images.SetKeyName(1, "");
            this.chkimage.Images.SetKeyName(2, "");
            // 
            // SchemaViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.schemaTree);
            this.Name = "SchemaViewer";
            this.Size = new System.Drawing.Size(326, 557);
            ((System.ComponentModel.ISupportInitialize)(this.schemaTree)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDSBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mainDS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTreeList.TreeList schemaTree;
        private System.Windows.Forms.ImageList dbImageIcons;
        private System.ComponentModel.BackgroundWorker bgGetSchema;
        private System.Windows.Forms.BindingSource mainDSBindingSource;
        private Datasets.MainDS mainDS;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colFullName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSchemaType;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colDataColumnID;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colName;
        private DevExpress.XtraTreeList.Columns.TreeListColumn colSchemaName;
        private System.Windows.Forms.ImageList chkimage;
        private DevExpress.XtraTreeList.Columns.TreeListColumn treeListColumn1;
    }
}
