namespace CodeVista
{
    partial class MainFormOld
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectObjectExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectObjectExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.templatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssCodeVista = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.test = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.mainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.schemaViewer2 = new CodeVista.Controls.SchemaViewer();
            this.btnGenerateHTML2 = new System.Windows.Forms.Button();
            this.btnUpdateAppSettings = new System.Windows.Forms.Button();
            this.btnGenerateHTML = new System.Windows.Forms.Button();
            this.btnGenerateComponents = new System.Windows.Forms.Button();
            this.btnGenerateAngularService = new System.Windows.Forms.Button();
            this.btnGenerateApiController = new System.Windows.Forms.Button();
            this.btnGenerateMapperConfig = new System.Windows.Forms.Button();
            this.btnGenerateAngularEntity = new System.Windows.Forms.Button();
            this.btnGenerateBindingModel = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNamespacePrefix = new System.Windows.Forms.TextBox();
            this.btnGenerateLogic = new System.Windows.Forms.Button();
            this.btnModifyResourceFile = new System.Windows.Forms.Button();
            this.btnGenerateEntities = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSolutionPath = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).BeginInit();
            this.mainSplitContainer.Panel1.SuspendLayout();
            this.mainSplitContainer.Panel2.SuspendLayout();
            this.mainSplitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.templatesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(802, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectObjectExplorerToolStripMenuItem,
            this.disconnectObjectExplorerToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // connectObjectExplorerToolStripMenuItem
            // 
            this.connectObjectExplorerToolStripMenuItem.Name = "connectObjectExplorerToolStripMenuItem";
            this.connectObjectExplorerToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.connectObjectExplorerToolStripMenuItem.Text = "Connect Object Explorer";
            this.connectObjectExplorerToolStripMenuItem.Click += new System.EventHandler(this.connectObjectExplorerToolStripMenuItem_Click);
            // 
            // disconnectObjectExplorerToolStripMenuItem
            // 
            this.disconnectObjectExplorerToolStripMenuItem.Name = "disconnectObjectExplorerToolStripMenuItem";
            this.disconnectObjectExplorerToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.disconnectObjectExplorerToolStripMenuItem.Text = "Disconnect Object Explorer";
            this.disconnectObjectExplorerToolStripMenuItem.Click += new System.EventHandler(this.disconnectObjectExplorerToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(213, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(216, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // templatesToolStripMenuItem
            // 
            this.templatesToolStripMenuItem.Name = "templatesToolStripMenuItem";
            this.templatesToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.templatesToolStripMenuItem.Text = "Templates";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssCodeVista,
            this.tssStatus,
            this.tssProgress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 518);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(802, 25);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssCodeVista
            // 
            this.tssCodeVista.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.tssCodeVista.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.tssCodeVista.Name = "tssCodeVista";
            this.tssCodeVista.Size = new System.Drawing.Size(85, 20);
            this.tssCodeVista.Text = "Code Vista 1.0";
            // 
            // tssStatus
            // 
            this.tssStatus.Name = "tssStatus";
            this.tssStatus.Size = new System.Drawing.Size(61, 20);
            this.tssStatus.Text = "Idle Status";
            // 
            // tssProgress
            // 
            this.tssProgress.AutoToolTip = true;
            this.tssProgress.Name = "tssProgress";
            this.tssProgress.Size = new System.Drawing.Size(100, 19);
            this.tssProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(54, 3);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(75, 23);
            this.test.TabIndex = 4;
            this.test.Text = "test";
            this.test.UseVisualStyleBackColor = true;
            this.test.Visible = false;
            this.test.Click += new System.EventHandler(this.test_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(135, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(126, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Generate BE";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // mainSplitContainer
            // 
            this.mainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainSplitContainer.Location = new System.Drawing.Point(0, 24);
            this.mainSplitContainer.Name = "mainSplitContainer";
            // 
            // mainSplitContainer.Panel1
            // 
            this.mainSplitContainer.Panel1.Controls.Add(this.schemaViewer2);
            // 
            // mainSplitContainer.Panel2
            // 
            this.mainSplitContainer.Panel2.Controls.Add(this.richTextBox1);
            this.mainSplitContainer.Panel2.Controls.Add(this.btnGenerateHTML2);
            this.mainSplitContainer.Panel2.Controls.Add(this.btnUpdateAppSettings);
            this.mainSplitContainer.Panel2.Controls.Add(this.btnGenerateHTML);
            this.mainSplitContainer.Panel2.Controls.Add(this.btnGenerateComponents);
            this.mainSplitContainer.Panel2.Controls.Add(this.btnGenerateAngularService);
            this.mainSplitContainer.Panel2.Controls.Add(this.btnGenerateApiController);
            this.mainSplitContainer.Panel2.Controls.Add(this.btnGenerateMapperConfig);
            this.mainSplitContainer.Panel2.Controls.Add(this.btnGenerateAngularEntity);
            this.mainSplitContainer.Panel2.Controls.Add(this.btnGenerateBindingModel);
            this.mainSplitContainer.Panel2.Controls.Add(this.label3);
            this.mainSplitContainer.Panel2.Controls.Add(this.txtNamespacePrefix);
            this.mainSplitContainer.Panel2.Controls.Add(this.btnGenerateLogic);
            this.mainSplitContainer.Panel2.Controls.Add(this.btnModifyResourceFile);
            this.mainSplitContainer.Panel2.Controls.Add(this.btnGenerateEntities);
            this.mainSplitContainer.Panel2.Controls.Add(this.label1);
            this.mainSplitContainer.Panel2.Controls.Add(this.txtSolutionPath);
            this.mainSplitContainer.Panel2.Controls.Add(this.button2);
            this.mainSplitContainer.Panel2.Controls.Add(this.test);
            this.mainSplitContainer.Panel2.Controls.Add(this.button1);
            this.mainSplitContainer.Size = new System.Drawing.Size(802, 494);
            this.mainSplitContainer.SplitterDistance = 266;
            this.mainSplitContainer.TabIndex = 6;
            // 
            // schemaViewer2
            // 
            this.schemaViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.schemaViewer2.Location = new System.Drawing.Point(0, 0);
            this.schemaViewer2.Name = "schemaViewer2";
            this.schemaViewer2.Project = null;
            this.schemaViewer2.Size = new System.Drawing.Size(266, 494);
            this.schemaViewer2.TabIndex = 0;
            // 
            // btnGenerateHTML2
            // 
            this.btnGenerateHTML2.Location = new System.Drawing.Point(352, 358);
            this.btnGenerateHTML2.Name = "btnGenerateHTML2";
            this.btnGenerateHTML2.Size = new System.Drawing.Size(148, 23);
            this.btnGenerateHTML2.TabIndex = 34;
            this.btnGenerateHTML2.Text = "Generate HTML-groups";
            this.btnGenerateHTML2.UseVisualStyleBackColor = true;
            this.btnGenerateHTML2.Click += new System.EventHandler(this.btnGenerateHTML2_Click);
            // 
            // btnUpdateAppSettings
            // 
            this.btnUpdateAppSettings.Location = new System.Drawing.Point(351, 213);
            this.btnUpdateAppSettings.Name = "btnUpdateAppSettings";
            this.btnUpdateAppSettings.Size = new System.Drawing.Size(148, 23);
            this.btnUpdateAppSettings.TabIndex = 33;
            this.btnUpdateAppSettings.Text = "Update AppSettnigs File";
            this.btnUpdateAppSettings.UseVisualStyleBackColor = true;
            this.btnUpdateAppSettings.Click += new System.EventHandler(this.btnUpdateAppSettings_Click);
            // 
            // btnGenerateHTML
            // 
            this.btnGenerateHTML.Location = new System.Drawing.Point(351, 329);
            this.btnGenerateHTML.Name = "btnGenerateHTML";
            this.btnGenerateHTML.Size = new System.Drawing.Size(148, 23);
            this.btnGenerateHTML.TabIndex = 32;
            this.btnGenerateHTML.Text = "Generate HTML-simple";
            this.btnGenerateHTML.UseVisualStyleBackColor = true;
            this.btnGenerateHTML.Click += new System.EventHandler(this.btnGenerateHTML_Click);
            // 
            // btnGenerateComponents
            // 
            this.btnGenerateComponents.Location = new System.Drawing.Point(351, 300);
            this.btnGenerateComponents.Name = "btnGenerateComponents";
            this.btnGenerateComponents.Size = new System.Drawing.Size(148, 23);
            this.btnGenerateComponents.TabIndex = 31;
            this.btnGenerateComponents.Text = "Generate Components";
            this.btnGenerateComponents.UseVisualStyleBackColor = true;
            this.btnGenerateComponents.Click += new System.EventHandler(this.btnGenerateComponents_Click);
            // 
            // btnGenerateAngularService
            // 
            this.btnGenerateAngularService.Location = new System.Drawing.Point(351, 271);
            this.btnGenerateAngularService.Name = "btnGenerateAngularService";
            this.btnGenerateAngularService.Size = new System.Drawing.Size(148, 23);
            this.btnGenerateAngularService.TabIndex = 28;
            this.btnGenerateAngularService.Text = "Generate Angular Service";
            this.btnGenerateAngularService.UseVisualStyleBackColor = true;
            this.btnGenerateAngularService.Click += new System.EventHandler(this.btnGenerateAngularService_Click);
            // 
            // btnGenerateApiController
            // 
            this.btnGenerateApiController.Location = new System.Drawing.Point(351, 242);
            this.btnGenerateApiController.Name = "btnGenerateApiController";
            this.btnGenerateApiController.Size = new System.Drawing.Size(148, 23);
            this.btnGenerateApiController.TabIndex = 25;
            this.btnGenerateApiController.Text = "Generate API Controller";
            this.btnGenerateApiController.UseVisualStyleBackColor = true;
            this.btnGenerateApiController.Click += new System.EventHandler(this.btnGenerateApiController_Click);
            // 
            // btnGenerateMapperConfig
            // 
            this.btnGenerateMapperConfig.Location = new System.Drawing.Point(351, 184);
            this.btnGenerateMapperConfig.Name = "btnGenerateMapperConfig";
            this.btnGenerateMapperConfig.Size = new System.Drawing.Size(148, 23);
            this.btnGenerateMapperConfig.TabIndex = 22;
            this.btnGenerateMapperConfig.Text = "Update WebApiConfig File";
            this.btnGenerateMapperConfig.UseVisualStyleBackColor = true;
            this.btnGenerateMapperConfig.Click += new System.EventHandler(this.btnGenerateMapperConfig_Click);
            // 
            // btnGenerateAngularEntity
            // 
            this.btnGenerateAngularEntity.Location = new System.Drawing.Point(352, 155);
            this.btnGenerateAngularEntity.Name = "btnGenerateAngularEntity";
            this.btnGenerateAngularEntity.Size = new System.Drawing.Size(148, 23);
            this.btnGenerateAngularEntity.TabIndex = 19;
            this.btnGenerateAngularEntity.Text = "Generate Angular Entity";
            this.btnGenerateAngularEntity.UseVisualStyleBackColor = true;
            this.btnGenerateAngularEntity.Click += new System.EventHandler(this.btnGenerateAngularEntity_Click);
            // 
            // btnGenerateBindingModel
            // 
            this.btnGenerateBindingModel.Location = new System.Drawing.Point(352, 126);
            this.btnGenerateBindingModel.Name = "btnGenerateBindingModel";
            this.btnGenerateBindingModel.Size = new System.Drawing.Size(148, 23);
            this.btnGenerateBindingModel.TabIndex = 16;
            this.btnGenerateBindingModel.Text = "Generate Binding Model";
            this.btnGenerateBindingModel.UseVisualStyleBackColor = true;
            this.btnGenerateBindingModel.Click += new System.EventHandler(this.btnGenerateBindingModel_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 113);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Namespace Prefix";
            // 
            // txtNamespacePrefix
            // 
            this.txtNamespacePrefix.Location = new System.Drawing.Point(54, 129);
            this.txtNamespacePrefix.Name = "txtNamespacePrefix";
            this.txtNamespacePrefix.Size = new System.Drawing.Size(292, 20);
            this.txtNamespacePrefix.TabIndex = 14;
            // 
            // btnGenerateLogic
            // 
            this.btnGenerateLogic.Location = new System.Drawing.Point(352, 97);
            this.btnGenerateLogic.Name = "btnGenerateLogic";
            this.btnGenerateLogic.Size = new System.Drawing.Size(148, 23);
            this.btnGenerateLogic.TabIndex = 13;
            this.btnGenerateLogic.Text = "Generate Logic";
            this.btnGenerateLogic.UseVisualStyleBackColor = true;
            this.btnGenerateLogic.Click += new System.EventHandler(this.btnGenerateLogic_Click);
            // 
            // btnModifyResourceFile
            // 
            this.btnModifyResourceFile.Location = new System.Drawing.Point(352, 411);
            this.btnModifyResourceFile.Name = "btnModifyResourceFile";
            this.btnModifyResourceFile.Size = new System.Drawing.Size(148, 23);
            this.btnModifyResourceFile.TabIndex = 12;
            this.btnModifyResourceFile.Text = "Modify Resource File";
            this.btnModifyResourceFile.UseVisualStyleBackColor = true;
            this.btnModifyResourceFile.Click += new System.EventHandler(this.btnModifyResourceFile_Click);
            // 
            // btnGenerateEntities
            // 
            this.btnGenerateEntities.Location = new System.Drawing.Point(351, 65);
            this.btnGenerateEntities.Name = "btnGenerateEntities";
            this.btnGenerateEntities.Size = new System.Drawing.Size(149, 23);
            this.btnGenerateEntities.TabIndex = 11;
            this.btnGenerateEntities.Text = "Generate Entities";
            this.btnGenerateEntities.UseVisualStyleBackColor = true;
            this.btnGenerateEntities.Click += new System.EventHandler(this.btnGenerateEntities_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Solution Path";
            // 
            // txtSolutionPath
            // 
            this.txtSolutionPath.Location = new System.Drawing.Point(54, 67);
            this.txtSolutionPath.Name = "txtSolutionPath";
            this.txtSolutionPath.Size = new System.Drawing.Size(292, 20);
            this.txtSolutionPath.TabIndex = 7;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(267, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(126, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Generate Complete Layers";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Location = new System.Drawing.Point(58, 172);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(287, 262);
            this.richTextBox1.TabIndex = 35;
            this.richTextBox1.Text = "";
            this.richTextBox1.Visible = false;
            // 
            // MainFormOld
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 543);
            this.Controls.Add(this.mainSplitContainer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainFormOld";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Form";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.mainSplitContainer.Panel1.ResumeLayout(false);
            this.mainSplitContainer.Panel2.ResumeLayout(false);
            this.mainSplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mainSplitContainer)).EndInit();
            this.mainSplitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssCodeVista;
        private System.Windows.Forms.ToolStripStatusLabel tssStatus;
        private System.Windows.Forms.ToolStripProgressBar tssProgress;
        
        private System.Windows.Forms.Button test;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectObjectExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectObjectExplorerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.SplitContainer mainSplitContainer;
        private System.Windows.Forms.ToolStripMenuItem templatesToolStripMenuItem;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSolutionPath;
        private System.Windows.Forms.Button btnGenerateEntities;
        private System.Windows.Forms.Button btnModifyResourceFile;
        private System.Windows.Forms.Button btnGenerateLogic;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnGenerateBindingModel;
        private System.Windows.Forms.Button btnGenerateAngularEntity;
        private System.Windows.Forms.Button btnGenerateMapperConfig;
        private System.Windows.Forms.Button btnGenerateAngularService;
        private System.Windows.Forms.Button btnGenerateApiController;
        private System.Windows.Forms.Button btnGenerateComponents;
        private System.Windows.Forms.TextBox txtNamespacePrefix;
        private System.Windows.Forms.Button btnGenerateHTML;
        private Controls.SchemaViewer schemaViewer2;
        private System.Windows.Forms.Button btnGenerateHTML2;
        private System.Windows.Forms.Button btnUpdateAppSettings;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

