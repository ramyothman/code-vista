using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Common.Generation;
using Common.Entities.MetaDataSchema;
using System.Xml.Linq;
using System.Linq;
using System.Linq.Expressions;
namespace CodeVista
{
    public partial class MainFormOld : Form
    {
        private XsltParam[] _namespaces;
        public MainFormOld()
        {
            InitializeComponent();
        }
        Common.Entities.MetaDataSchema.Project project = new Common.Entities.MetaDataSchema.Project();
        private void test_Click(object sender, EventArgs e)
        {
            
            project.ServerName = @"RBM-MLAP";
            project.UserName = "sa";
            project.Password = "welcome";
            project.IsWindowsAuthentication = false;

            project.Name = "Yarab";

            project.CreateDate = System.DateTime.Now;
            DataType.LoadHashTables(Application.ExecutablePath.Substring(0,Application.ExecutablePath.LastIndexOf("\\")) + "\\" + "DataTypesMapper.xml");
            project.DatabaseType = DatabaseTypes.SQLServer;
            project.Connect();
            project.ExtractorManager.DatabaseReaders.GetDatabases(project);
            schemaViewer2.Project = project;
            //schemaViewer2.GetSchemaBackGround();
            schemaViewer2.BuildDatabases();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string input = @"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Qiyas.BusinessLogicLayer.Entity;

namespace BusinessLogicLayer.Entities.<%$$ SchemaName $$%>
{
    [DataObject(true)]
	[Serializable]
    public class <%$$ TableName $$%> : EntityBase<Qiyas.DataAccessLayer.<%$$ TableName $$%>>
    {
        #region Constructors
        public <%$$ TableName $$%>()
        {
            this.entity = new Qiyas.DataAccessLayer.<%$$ TableName $$%>();
            isNew = true;
        }

        public <%$$ TableName $$%>([$$ <%$$ TableColumn(PK),DataType $$%> <%$$ TableColumn(PK),Name(FL) $$%> $$])
        {
			this.entity = context.<%$$ TableName $$%>s.Where([$$p => p.<%$$ TableColumn(PK),Name $$%> == <%$$ TableColumn(PK),Name(FL) $$%> $$]).FirstOrDefault();
        }

        internal <%$$ TableName $$%>(Qiyas.DataAccessLayer.<%$$ TableName $$%> entity)
        {
            this.entity = entity;
        }
        #endregion

        #region Properties
		[$$ 
			/// <summary>
			/// This Property represents the <%$$ TableColumn,Name(FU) $$%> which has <%$$ TableColumn,DataType $$%> type
			/// </summary>
			
            public <%$$ TableColumn,DataType $$%> <%$$ TableColumn,Name(FU) $$%>
            {
                
                set
                {
					this.entity.<%$$ TableColumn,Name $$%> = value;
                }
                get
                {
                    return this.entity.<%$$ TableColumn,Name $$%>;
                }
            }
        <%$$ NewLine $$%>
        $$]
        #endregion

		#region Methods
        internal override bool? Save(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (isNew)
            {
                context.<%$$ TableName $$%>s.InsertOnSubmit(this.entity);
            }
            else
            {
                
            }
            if (commit)
                try
                {
                    context.SubmitChanges();
                    isNew = false;
                    return true;
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    return false;
                }
            else
                return null;

        }
        internal override bool? Delete(Qiyas.DataAccessLayer.QiyasLinqDataContext context, bool commit)
        {
            if (!isNew)
                context.<%$$ TableName $$%>s.DeleteOnSubmit(this.entity);
            else
                return false;
            if (commit)
                try
                {
                    context.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    return false;
                }
            else
                return null;
        }
        public bool Save()
        {
            return Save(context, true).Value;
        }
        public bool Delete()
        {
            return Delete(context, true).Value;
        }
        #endregion
        
    }
}
";

            Database selectedDb = schemaViewer2.SelectedDatabase;
            Database testDB = selectedDb.Clone() as Database;
            Generator g = new Generator(CodeType.CSharp);
            Database db = schemaViewer2.Project.GetDatabaseByName("Qiyas");
            
            Table table = new Table();
            table.Parent = db;
            foreach(string s in schemaViewer2.SelectedTables)
            {
                table = Common.Entities.Entity.GetbyName(s, selectedDb.Tables);
                string code = g.GetCode(input, "BusinessLogicLayer.Entities", "Persons", table);    
                
            }
            
            
        }

        private bool ColumnExists(Column col, Table table)
        {
            foreach (Column c in table.Columns)
            {
                if (c.Name == col.Name)
                    return true;
            }
            return false;
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
                schemaViewer2.Project = project;
                schemaViewer2.BuildDatabases();
            }
        }

        private void disconnectObjectExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            schemaViewer2.Project = null;
            project = new Project();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo infoSave = new System.IO.DirectoryInfo(@"C:\IComputer\ISuperWork\Work\Personal Projects\CodeVista\CodeVista\bin\Debug\PamsNew\Save");
            Database selectedDb = schemaViewer2.SelectedDatabase;
            Database testDB = selectedDb.Clone() as Database;
            Generator g = new Generator(CodeType.CSharp);

            foreach(Table table in selectedDb.Tables)
            {
                if(table.EntitySelected)
                {
                    foreach (System.IO.FileInfo info in infoSave.GetFiles())
                    {
                        string name = table.Name.LastIndexOf("s") == (table.Name.Length - 1) ? table.Name.Remove(table.Name.Length - 1, 1) : table.Name;
                        if (info.Name.Contains(name))
                        {
                            info.CopyTo(txtSolutionPath.Text + @"\" + info.Name, true);
                        }
                        //Database db = schemaViewer2.Project.GetDatabaseByName("ShotecALL");
                    }
                }
            }
            
            System.IO.DirectoryInfo infoView = new System.IO.DirectoryInfo(@"C:\IComputer\ISuperWork\Work\Personal Projects\CodeVista\CodeVista\bin\Debug\PamsNew\View");
            foreach (Table table in selectedDb.Tables)
            {
                if (table.EntitySelected)
                {
                    foreach (System.IO.FileInfo info in infoView.GetFiles())
                    {
                        string name = table.Name.LastIndexOf("s") == (table.Name.Length - 1) ? table.Name.Remove(table.Name.Length - 1, 1) : table.Name;
                        if (info.Name.Contains(name))
                        {
                            info.CopyTo(txtSolutionPath.Text + @"\" + info.Name, true);
                        }
                        //Database db = schemaViewer2.Project.GetDatabaseByName("ShotecALL");
                    }
                }
            }
        }

        private void btnGenerateEntities_Click(object sender, EventArgs e)
        {
            Database selectedDb = schemaViewer2.SelectedDatabase;
            List<string> tables = schemaViewer2.SelectedTables;
            string message = "";
            setNamespaces();

            foreach (string t in tables)
            {
                Database db = selectedDb.Clone(t) as Database;
                string schema = Table.GetByName(db.Tables, t).Schema;
                string pathInSolution;
                if (string.IsNullOrEmpty(txtNamespacePrefix.Text))
                    pathInSolution = "\\Logic\\Business\\Entity\\"+schema;
                else
                    pathInSolution = "\\Logic\\"+ txtNamespacePrefix.Text + "."+"Business\\Entity\\" +  schema ;
                string xml = Database.SerializeToString(db);
                
                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.LoadXml(xml);
                if (System.IO.Directory.Exists(txtSolutionPath.Text + pathInSolution))
                {
                    message = "Files Generated";
                    //C:\IComputer\IWork\QiyasPPM\QiyasPlatform\Qiyas.BusinessLogicLayer\Entity\PPM
                    if (!System.IO.File.Exists(txtSolutionPath.Text + t + ".cs"))
                        Common.Generation.XsltGenerator.GenerateViaXSLT("Pams/EntityTemplateEmpty.xslt", document, txtSolutionPath.Text + pathInSolution + "\\" + t + ".cs", _namespaces);
                    Common.Generation.XsltGenerator.GenerateViaXSLT("Pams/EntityTemplate.xslt", document, txtSolutionPath.Text + pathInSolution + "\\" + t + ".Model.cs",_namespaces);
                }
                else
                {
                    message = "Error! Folder not Found. Please Create it First";
                    break;
                }
            }
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);
        }

        private void btnModifyResourceFile_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                 <data name="Code" xml:space="preserve">
                    <value>«·ﬂÊœ</value>
                  </data>
                 */
                Database selectedDb = schemaViewer2.SelectedDatabase;
                List<string> tables = schemaViewer2.SelectedTables;
                if (!System.IO.File.Exists(txtSolutionPath.Text))
                    return;
                XElement root = XElement.Load(txtSolutionPath.Text);
                foreach (string t in tables)
                {
                    Database db = selectedDb.Clone(t) as Database;
                    foreach(Table tb in db.Tables)
                    {
                        foreach(Column col in tb.Columns)
                        {
                            XElement data =
                                (from el in root.Elements("data")
                                 where (string)el.Attribute("name").ToString().ToLower() == col.ColumnFullName.ToLower()
                                select el).FirstOrDefault();
                            if(data == null)
                            {
                                XElement ndata = new XElement("data");
                                //ndata.SetAttributeValue("xml:space", "preserve");
                                ndata.SetAttributeValue("name", col.ColumnFullName);
                                XElement val = new XElement("value");
                                val.Value = col.Name;
                                ndata.Add(val);
                                //var relem = root.Element("root");
                                root.Add(ndata);

                            }
                        }

                    }
                }
                root.Save(txtSolutionPath.Text);
                MessageBox.Show("Files Generated");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void btnGenerateLogic_Click(object sender, EventArgs e)
        {
            Database selectedDb = schemaViewer2.SelectedDatabase;
            List<string> tables = schemaViewer2.SelectedTables;
            string message="";
            setNamespaces();

            foreach (string t in tables)
            {
                Database db = selectedDb.Clone(t) as Database;
                string schema = Table.GetByName(db.Tables, t).Schema;
                string pathInSolution;
                if (string.IsNullOrEmpty(txtNamespacePrefix.Text))
                    pathInSolution = "\\Logic\\Business\\Component\\" + schema;
                else
                    pathInSolution = "\\Logic\\" + txtNamespacePrefix.Text + "." + "Business\\Component\\" + schema;
                string xml = Database.SerializeToString(db);
                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.LoadXml(xml);
                if (System.IO.Directory.Exists(txtSolutionPath.Text + pathInSolution))
                {
                    message = "Files Generated";
                    //C:\IComputer\IWork\QiyasPPM\QiyasPlatform\Qiyas.BusinessLogicLayer\Entity\PPM
                    if (!System.IO.File.Exists(txtSolutionPath.Text + pathInSolution + t + ".cs"))
                        Common.Generation.XsltGenerator.GenerateViaXSLT("Pams/QueryTemplateEmpty.xslt", document, txtSolutionPath.Text + pathInSolution + "\\" + t + "Logic.cs", _namespaces);
                    Common.Generation.XsltGenerator.GenerateViaXSLT("Pams/QueryTemplate.xslt", document, txtSolutionPath.Text + pathInSolution + "\\" + t + "Logic.Logic.cs", _namespaces);
                }
                else
                {
                    message = "Error! Folder not Found. Please Create it First";
                    break;
                }
                
            }
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);
          
        }

        private void btnGenerateBindingModel_Click(object sender, EventArgs e)
        {
            Database selectedDb = schemaViewer2.SelectedDatabase;
            List<string> tables = schemaViewer2.SelectedTables;
            string message = "";
            setNamespaces();

            foreach (string t in tables)
            {
                Database db = selectedDb.Clone(t) as Database;
                string schema = Table.GetByName(db.Tables, t).Schema;
                string pathInSolution;
                if (string.IsNullOrEmpty(txtNamespacePrefix.Text))
                    pathInSolution = "\\API\\Models\\BindingModels\\" + schema;
                else
                    pathInSolution = "\\API\\" + txtNamespacePrefix.Text + "." + "API\\Models\\BindingModels\\" + schema;
                string xml = Database.SerializeToString(db);
                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.LoadXml(xml);
                if (System.IO.Directory.Exists(txtSolutionPath.Text + pathInSolution))
                {
                    message = "Files Generated";
                    Common.Generation.XsltGenerator.GenerateViaXSLT("Pams/BindingModelTemplate.xslt", document, txtSolutionPath.Text + pathInSolution + "\\" + t + "BindingModel.cs", _namespaces);
                }
                else
                {
                    message = "Error! Folder not Found. Please Create it First";
                    break;
                }
            }
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);
        }

        private void btnGenerateAngularEntity_Click(object sender, EventArgs e)
        {
            Database selectedDb = schemaViewer2.SelectedDatabase;
            List<string> tables = schemaViewer2.SelectedTables;
            string message = "";

            foreach (string t in tables)
            {
                Database db = selectedDb.Clone(t) as Database;
                string schema = Table.GetByName(db.Tables, t).Schema;
                string pathInSolution;
                if (string.IsNullOrEmpty(txtNamespacePrefix.Text))
                    pathInSolution = "\\Web\\ClientApp\\app\\models\\" + schema;
                else
                    pathInSolution = "\\Web\\" + txtNamespacePrefix.Text + "." + "Web\\ClientApp\\app\\models\\" + schema;
               
                string xml = Database.SerializeToString(db);
                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.LoadXml(xml);
                var table = db.Tables.Find(x => x.Name == t);
                string loweredName = t;
                if (table != null)
                    loweredName = table.NameWithDashesLowered;
                if (System.IO.Directory.Exists(txtSolutionPath.Text + pathInSolution))
                {
                    message = "Files Generated";
                    Common.Generation.XsltGenerator.GenerateViaXSLT("Pams/AngularModel.xslt", document, txtSolutionPath.Text + pathInSolution + "\\" + loweredName + ".model.ts");
                }
                else
                {
                    message="Error! Folder not Found. Please Create it First";
                    break;
                }
            }
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);
        }

        private void btnGenerateMapperConfig_Click(object sender, EventArgs e)
        {
            Database selectedDb = schemaViewer2.SelectedDatabase;
            List<string> tables = schemaViewer2.SelectedTables;
            List<string> schemas = new List<string>();
            if (!System.IO.File.Exists(txtSolutionPath.Text))
            {
                MessageBox.Show("File not Found, Please Create it first");
                return;
            }
            setNamespaces();
            string fileText = System.IO.File.ReadAllText(txtSolutionPath.Text);
            StringBuilder mappingStr = new StringBuilder();
            StringBuilder apiPathStr = new StringBuilder();
            string tempFile = "generateMapperConfig.txt";

            foreach (string t in tables)
            {
                Database db = selectedDb.Clone(t) as Database;
                string xml = Database.SerializeToString(db);
                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.LoadXml(xml);
                
                Common.Generation.XsltGenerator.GenerateViaXSLT("Pams/MapperConfigTemplate.xslt", document, tempFile,_namespaces);
                string fileTempContent = System.IO.File.ReadAllText(tempFile);
                System.IO.File.Delete(tempFile);
                if(!fileText.Contains(fileTempContent))
                    mappingStr.AppendLine(fileTempContent);

                string schema = Table.GetByName(db.Tables, t).Schema;
                string schemaApiPath = "config.Routes.MapHttpRoute(\n\t\t\t\t name: \""+schema+"\",\n\t\t\t\t routeTemplate: \""+schema+"/{controller}/{action}/{id}\",\n\t\t\t\t  defaults: new { id = RouteParameter.Optional } \n\t\t\t );\n";
                if (!fileText.Contains("\""+schema+"/{controller}/{action}/{id}\"") && !apiPathStr.ToString().Contains(schemaApiPath))   //Better than searching the full string becasue indentation is not ignored
                    apiPathStr.Append(schemaApiPath);
            }

            //Writing the Mappings to the file            
            if(fileText.Contains("/*Add Mappers for new Tables Here*/") && fileText.Contains("/*Add Mappers for new Tables Here*/"))
            {
                if (string.IsNullOrEmpty(mappingStr.ToString()) || string.IsNullOrWhiteSpace(mappingStr.ToString()))
                    MessageBox.Show("Mapping for the selected Tables already exits!");
                else
                    {
                        mappingStr.AppendLine("            /*Add Mappers for new Tables Here*/");
                        fileText = fileText.Replace("/*Add Mappers for new Tables Here*/", mappingStr.ToString());
                    }

                if (string.IsNullOrEmpty(apiPathStr.ToString()) || string.IsNullOrWhiteSpace(apiPathStr.ToString()))
                    MessageBox.Show("Ne new schemas need to be added.");
                else
                    {
                        apiPathStr.AppendLine("            /*Add Path for new Schemas Here*/");
                        fileText = fileText.Replace("/*Add Path for new Schemas Here*/", apiPathStr.ToString());
                    }
            }
            else
            {
                MessageBox.Show("Please Add these comments to your file: \n -/*Add Mappers for new Tables Here*/ \n (where you want to generate the mapping). \n -/*Add Path for new Schemas Here*/ \n (where you want to generate the api path configuration).");
                return;
            }

             System.IO.File.WriteAllText(txtSolutionPath.Text, fileText);
             MessageBox.Show("File Updated");
        }

        private void btnGenerateApiController_Click(object sender, EventArgs e)
        {
            Database selectedDb = schemaViewer2.SelectedDatabase;
            List<string> tables = schemaViewer2.SelectedTables;
            string message = "";
            setNamespaces();

            foreach (string t in tables)
            {
                Database db = selectedDb.Clone(t) as Database;
                //db.ParentProject.NameSpace = "RKT.Portal.API.Controllers.General";
                string schema = Table.GetByName(db.Tables, t).Schema;
                string pathInSolution;
                if (string.IsNullOrEmpty(txtNamespacePrefix.Text))
                    pathInSolution = "\\API\\Controllers\\" + schema;
                else
                    pathInSolution = "\\API\\" + txtNamespacePrefix.Text + "." + "API\\Controllers\\" + schema;

                string xml = Database.SerializeToString(db);
                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.LoadXml(xml);

                if (System.IO.Directory.Exists(txtSolutionPath.Text + pathInSolution))
                {
                    //C:\IComputer\IWork\QiyasPPM\QiyasPlatform\Qiyas.BusinessLogicLayer\Entity\PPM
                    if (!System.IO.File.Exists(txtSolutionPath.Text + pathInSolution + "\\" + t + "Controller.cs"))
                     {
                        Common.Generation.XsltGenerator.GenerateViaXSLT("Pams/APIController.xslt", document, txtSolutionPath.Text + pathInSolution + "\\" + t + "Controller.cs",_namespaces);
                        message = "Files Generated";
                     }
                    else
                        message ="Error! Cannot Overwrite an Existing File";

                }
                else
                {
                    message="Error! Folder not Found. Please Create it First";
                    break;
                } 
            }
            MessageBox.Show(message);
        }

        private void btnGenerateAngularService_Click(object sender, EventArgs e)
        {
            Database selectedDb = schemaViewer2.SelectedDatabase;
            List<string> tables = schemaViewer2.SelectedTables;
            string message = "";

            foreach (string t in tables)
            {
                Database db = selectedDb.Clone(t) as Database;
                string schema = Table.GetByName(db.Tables, t).Schema;
                string pathInSolution;
                if (string.IsNullOrEmpty(txtNamespacePrefix.Text))
                    pathInSolution = "\\Web\\ClientApp\\app\\services\\" + schema;
                else
                    pathInSolution = "\\Web\\" + txtNamespacePrefix.Text + "." + "Web\\ClientApp\\app\\services\\" + schema;
                string xml = Database.SerializeToString(db);
                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.LoadXml(xml);
                var table = db.Tables.Find(x => x.Name == t);
                string loweredName = t;
                if (table != null)
                    loweredName = table.NameWithDashesLowered;
                if (System.IO.Directory.Exists(txtSolutionPath.Text + pathInSolution))
                {
                    message = "Files Generated";
                    //if (!System.IO.File.Exists(txtAngularService.Text + loweredName + ".service.ts"))
                    Common.Generation.XsltGenerator.GenerateViaXSLT("Pams/AngularService.xslt", document, txtSolutionPath.Text + pathInSolution + "\\" + loweredName + ".service.ts");
                }
                else
                {
                    message = "Error! Folder not Found. Please Create it First";
                    break;
                }
            }
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);
        }

        private void btnGenerateComponents_Click(object sender, EventArgs e)
        {
            Database selectedDb = schemaViewer2.SelectedDatabase;
            List<string> tables = schemaViewer2.SelectedTables;
            string message = "";

            foreach (string t in tables)
            {
                Database db = selectedDb.Clone(t) as Database;
                string schema = Table.GetByName(db.Tables, t).Schema;
                string pathInSolution;
                if (string.IsNullOrEmpty(txtNamespacePrefix.Text))
                    pathInSolution = "\\Web\\ClientApp\\app\\components\\" + schema;
                else
                    pathInSolution = "\\Web\\" + txtNamespacePrefix.Text + "." + "Web\\ClientApp\\app\\components\\" + schema;
                string xml = Database.SerializeToString(db);

                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.LoadXml(xml);
                var table = db.Tables.Find(x => x.Name == t);
                string loweredName = t;
                if (table != null)
                    loweredName = table.NameWithDashesLowered;
                if (System.IO.Directory.Exists(txtSolutionPath.Text + pathInSolution))
                {
                    message = "Files Generated";
                    Common.Generation.XsltGenerator.GenerateViaXSLT("Pams/ComponentTemplate.xslt", document, txtSolutionPath.Text  + pathInSolution + "\\" + loweredName + ".component.ts");
                }
                else
                {
                    message = "Error! Folder not Found. Please Create it First";
                    break;
                }
            }
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);
        }

        private void btnGenerateHTML_Click(object sender, EventArgs e)
        {
            Database selectedDb = schemaViewer2.SelectedDatabase; 
            List<string> tables = schemaViewer2.SelectedTables;
            string message = "";

            foreach (string t in tables)
            {
                Database db = selectedDb.Clone(t) as Database;
                string schema = Table.GetByName(db.Tables, t).Schema;
                string pathInSolution;
                if (string.IsNullOrEmpty(txtNamespacePrefix.Text))
                    pathInSolution = "\\Web\\ClientApp\\app\\views\\" + schema;
                else
                    pathInSolution = "\\Web\\" + txtNamespacePrefix.Text + "." + "Web\\ClientApp\\app\\views\\" + schema;
                string xml = Database.SerializeToString(db);

                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.LoadXml(xml);
                var table = db.Tables.Find(x => x.Name == t);
                string loweredName = t;
                if (table != null)
                    loweredName = table.NameWithDashesLowered;

                if (System.IO.Directory.Exists(txtSolutionPath.Text + pathInSolution))
                {
                    message = "Files Generated";
                    Common.Generation.XsltGenerator.GenerateViaXSLT("Pams/HtmlTemplate.xslt", document, txtSolutionPath.Text + pathInSolution + "\\" + loweredName + ".component.html");
                }
                else
                {
                    message = "Error! Folder not Found. Please Create it First";
                    break;
                }
            }
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);
        }

        private void btnUpdateAppSettings_Click(object sender, EventArgs e)
        {
            Database selectedDb = schemaViewer2.SelectedDatabase;
            List<string> tables = schemaViewer2.SelectedTables;
            if (!System.IO.File.Exists(txtSolutionPath.Text))
            {
                MessageBox.Show("File not Found, Please Create it first");
                return;
            }
            string fileText = System.IO.File.ReadAllText(txtSolutionPath.Text);
            StringBuilder apiUrlStr = new StringBuilder();
            string tempFile = "generateApiURLConfig.txt";

            foreach (string t in tables)
            {
                Database db = selectedDb.Clone(t) as Database;
                string xml = Database.SerializeToString(db);
                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.LoadXml(xml);

                Common.Generation.XsltGenerator.GenerateViaXSLT("Pams/AppSettingsAdditionsTemplate.xslt", document, tempFile);
                string fileTempContent = System.IO.File.ReadAllText(tempFile);
                System.IO.File.Delete(tempFile);
                if (!fileText.Contains(fileTempContent))
                    apiUrlStr.AppendLine(fileTempContent);
            }

            //Writing the Mappings to the file            
            if (fileText.Contains("/*Add Api Urls for new Tables Here*/"))
            {
                if (string.IsNullOrEmpty(apiUrlStr.ToString()) || string.IsNullOrWhiteSpace(apiUrlStr.ToString()))
                    MessageBox.Show("API Urls for the selected Tables already exit!");
                else
                {
                    apiUrlStr.AppendLine("            /*Add Api Urls for new Tables Here*/");
                    fileText = fileText.Replace("/*Add Api Urls for new Tables Here*/", apiUrlStr.ToString());
                }
            }
            else
            {
                MessageBox.Show("Please Add this comment to your file: \n -/*Add Api Urls for new Tables Here*/ \n (where you want to generate the Urls).");
                return;
            }

            System.IO.File.WriteAllText(txtSolutionPath.Text, fileText);
            MessageBox.Show("File Updated");
        }

        private void setNamespaces()
        {
            XsltParam param;
             _namespaces = new XsltParam[5];
            
            //set the path in the solution folder and all the namespaces
            if (string.IsNullOrEmpty(txtNamespacePrefix.Text))
            {
                param.Name = "EntityNamespace";
                param.Value = "Business.Entity";
                _namespaces[0] = param;

                param.Name = "LogicNamespace";
                param.Value = "Business.Components";
                _namespaces[1] = param;

                param.Name = "BindingModelNamespace";
                param.Value = "API.Models.BindingModels"; ;
                _namespaces[2] = param;

                param.Name = "APIControllerNamespace";
                param.Value = "Web.Controllers.WebApi";
                _namespaces[3] = param;

                param.Name = "Prefix";
                param.Value = "" ;
                _namespaces[4] = param;
            }
            else
            {
                param.Name = "EntityNamespace";
                param.Value = txtNamespacePrefix.Text + ".Business.Entity";
                _namespaces[0] = param;

                param.Name = "LogicNamespace";
                param.Value = txtNamespacePrefix.Text + ".Business.Components";
                _namespaces[1] = param;

                param.Name = "BindingModelNamespace";
                param.Value = txtNamespacePrefix.Text + ".API.Models.BindingModels"; ;
                _namespaces[2] = param;

                param.Name = "APIControllerNamespace";
                param.Value = txtNamespacePrefix.Text + ".Web.Controllers.WebApi";
                _namespaces[3] = param;

                param.Name = "Prefix";
                param.Value = txtNamespacePrefix.Text ;
                _namespaces[4] = param;
            }

        }

        private void btnGenerateHTML2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBox1.Text))
            {
                richTextBox1.Visible = true;
                richTextBox1.Text = "Button Usage:" + Environment.NewLine + "The description attribute of a column in the Design View of a table contains information about how and where the element is displayed in the html." + Environment.NewLine +
                                    "The possible formats are:" + Environment.NewLine + " -\"description\": the description field in the table." + Environment.NewLine +
                                     "- \"field x,y,m,n\": x is the order of the field in display.y is the span of the field(out of 12)." + Environment.NewLine +
                                     "m is whether the field is the 1st of a new group in display(0 or 1).n is whether the field is the start of a new row in a group or not." + Environment.NewLine +
                                     "Assumptions:" + Environment.NewLine + "- The primary key of a table consists of its name + \"ID\"." + Environment.NewLine +
                                     "- Every Table contains a field \"Name\"." + Environment.NewLine +
                                     "- 1st element is assumed to have the value of 0 for m and n." + Environment.NewLine +
                                     "If you are sure you want to use this html template, click the button again.Otherwise," + Environment.NewLine + "please use the \"Generate HTML-simple\" button";
            }
            else
            {
                richTextBox1.Visible = false;
                Database selectedDb = schemaViewer2.SelectedDatabase;
                List<string> tables = schemaViewer2.SelectedTables;
                string message = "";

                foreach (string t in tables)
                {
                    Database db = selectedDb.Clone(t) as Database;
                    string schema = Table.GetByName(db.Tables, t).Schema;
                    string pathInSolution;
                    if (string.IsNullOrEmpty(txtNamespacePrefix.Text))
                        pathInSolution = "\\Web\\ClientApp\\app\\views\\" + schema;
                    else
                        pathInSolution = "\\Web\\" + txtNamespacePrefix.Text + "." + "Web\\ClientApp\\app\\views\\" + schema;
                    string xml = Database.SerializeToString(db);

                    System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                    document.LoadXml(xml);
                    var table = db.Tables.Find(x => x.Name == t);
                    string loweredName = t;
                    if (table != null)
                        loweredName = table.NameWithDashesLowered;

                    if (System.IO.Directory.Exists(txtSolutionPath.Text + pathInSolution))
                    {
                        message = "Files Generated";
                        Common.Generation.XsltGenerator.GenerateViaXSLT("Pams/HtmlTemplate (2).xslt", document, txtSolutionPath.Text + pathInSolution + "\\" + loweredName + ".component.html");
                    }
                    else
                    {
                        message = "Error! Folder not Found. Please Create it First";
                        break;
                    }
                }
                if (!String.IsNullOrEmpty(message))
                    MessageBox.Show(message);
            }
        }
    }
}