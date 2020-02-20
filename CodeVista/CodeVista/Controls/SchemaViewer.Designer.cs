namespace CodeVista.Controls
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
            this.bgGetSchema = new System.ComponentModel.BackgroundWorker();
            this.schemaTree = new System.Windows.Forms.TreeView();
            this.dbImageIcons = new System.Windows.Forms.ImageList(this.components);
            this.SuspendLayout();
            // 
            // bgGetSchema
            // 
            this.bgGetSchema.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgGetSchema_DoWork);
            // 
            // schemaTree
            // 
            this.schemaTree.CheckBoxes = true;
            this.schemaTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schemaTree.ImageIndex = 0;
            this.schemaTree.ImageList = this.dbImageIcons;
            this.schemaTree.Location = new System.Drawing.Point(0, 0);
            this.schemaTree.Name = "schemaTree";
            this.schemaTree.SelectedImageIndex = 0;
            this.schemaTree.ShowLines = false;
            this.schemaTree.Size = new System.Drawing.Size(177, 455);
            this.schemaTree.TabIndex = 0;
            this.schemaTree.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.schemaTree_AfterCheck);
            this.schemaTree.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.schemaTree_BeforeExpand);
            this.schemaTree.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.schemaTree_BeforeCheck);
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
            // SchemaViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.schemaTree);
            this.Name = "SchemaViewer";
            this.Size = new System.Drawing.Size(177, 455);
            this.ResumeLayout(false);

        }

        #endregion

        private System.ComponentModel.BackgroundWorker bgGetSchema;
        private System.Windows.Forms.TreeView schemaTree;
        private System.Windows.Forms.ImageList dbImageIcons;
    }
}
