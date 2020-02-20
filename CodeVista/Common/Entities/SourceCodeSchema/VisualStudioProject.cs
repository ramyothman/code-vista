using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Common.Entities.SourceCodeSchema
{
    
    public class VisualStudioProject : Entities.Entity
    {
        
        public SolutionFile Solution
        {
            get { return Parent as SolutionFile; }
            set { _Parent = value; }
        }
        private string _ProjectTypeGUID;
        public string ProjectTypeGUID
        {
            set 
            {
                _ProjectTypeGUID = GetGuid(value); 
            }
            get { return _ProjectTypeGUID; }
        }
        private string _ProjectGUID;
        public string ProjectGuid
        {
            set { _ProjectGUID = GetGuid(value); }
            get { return _ProjectGUID; }
        }

        private string _ProjectPath;
        public string ProjectPath
        {
            set { _ProjectPath = value; }
            get { return _ProjectPath; }
        }

        private string _TargetFramework;
        public string TargetFramework
        {
            set { _TargetFramework = value; }
            get { return _TargetFramework; }
        }

        private CodeType _CodeType;
        public CodeType CodeType
        {
            set { _CodeType = value; }
            get { return _CodeType; }
        }

        private string _PlatForm;
        public string PlatForm
        {
            get { return _PlatForm; }
            set
            {
                _PlatForm = value;
            }
        }

        private string _ProductVersion;
        public string ProductVersion
        {
            get { return _ProductVersion; }
            set
            {
                _ProductVersion = value;
            }
        }

        private string _SchemaVersion;
        public string SchemaVersion
        {
            get { return _SchemaVersion; }
            set
            {
                _SchemaVersion = value;
            }
        }

        private string _OutputType;
        public string OutputType
        {
            get { return _OutputType; }
            set
            {
                _OutputType = value;
            }
        }

        private string _RootNamespace;
        public string RootNamespace
        {
            get { return _RootNamespace; }
            set
            {
                _RootNamespace = value;
            }
        }

        private string _AssemblyName;
        public string AssemblyName
        {
            get { return _AssemblyName; }
            set
            {
                _AssemblyName = value;
            }
        }

        

        public CompilerOptions DebugCompilerOptions = new CompilerOptions();
        public CompilerOptions ReleaseCompilerOptions = new CompilerOptions();

        private List<ProjectFolder> _ProjectFolders = new List<ProjectFolder>();
        public List<ProjectFolder> ProjectFolders
        {
            get { return _ProjectFolders; }
            set { _ProjectFolders = value; }
        }
        private List<ProjectFiles> _ProjectFiles = new List<ProjectFiles>();
        public List<ProjectFiles> ProjectFiles
        {
            get { return _ProjectFiles; }
            set { _ProjectFiles = value; }
        }
        private List<ProjectReference> _ProjectReferences = null;
        public List<ProjectReference> ProjectReferences
        {
            set { _ProjectReferences = value; }
            get { return _ProjectReferences; }
        }

        public void LoadProject(string path)
        {
            //ProjectPath = path;
            string fullPath = "";
            if (Solution != null)
            {
                fullPath = Solution.SolutionPath.Replace(Solution.Name, "");
            }
            if (ProjectPath.EndsWith(".csproj") || ProjectPath.EndsWith(".vbproj"))
            {
                #region Reading Project
                fullPath = fullPath + ProjectPath;
                System.Xml.XmlDocument document = new System.Xml.XmlDocument();
                document.Load(fullPath);
                System.Xml.XmlNodeList itemGroupNodeList = document.GetElementsByTagName("ItemGroup");
                foreach (System.Xml.XmlNode node in itemGroupNodeList)
                {
                    foreach (System.Xml.XmlNode nodeChild in node.ChildNodes)
                    {
                        try
                        {
                            if (nodeChild.Name.ToLower() == "reference")
                            {
                                ProjectReference reference = new ProjectReference();
                                reference.Include = nodeChild.Attributes["Include"].Value;
                                if (this.ProjectReferences == null)
                                    this.ProjectReferences = new List<ProjectReference>();
                                this.ProjectReferences.Add(reference);
                            }
                            else if (nodeChild.Name.ToLower() == "compile")
                            {
                                string []fullFilePaths = nodeChild.Attributes["Include"].Value.Split('\\');
                                int count = 1;
                                ProjectFolder folder = null;
                                foreach (string str in fullFilePaths)
                                {
                                    if (fullFilePaths.Length == count)
                                    {
                                        ProjectFiles file = new ProjectFiles();
                                        file.Include = nodeChild.Attributes["Include"].Value;
                                        int index = file.Include.LastIndexOf('\\');
                                        index++;
                                        if (index < 0)
                                            index = 0;

                                        file.Name = file.Include.Substring(index, file.Include.Length - index);
                                        if (folder == null)
                                            this.ProjectFiles.Add(file);
                                        else
                                            folder.Files.Add(file);
                                    }
                                    else
                                    {
                                        bool Exists = false;
                                        ProjectFolder parentFolder = folder;
                                        if(parentFolder == null)
                                            folder = ProjectFolder.GetFolder(str,this.ProjectFolders,out Exists);
                                        else
                                            folder = ProjectFolder.GetFolder(str, parentFolder.Folders,out Exists);
                                        if (!Exists)
                                        {
                                            folder.Parent = parentFolder;
                                            if (parentFolder == null)
                                                this.ProjectFolders.Add(folder);
                                            else
                                                parentFolder.Folders.Add(folder);
                                        }
                                    }
                                    count++;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
                #endregion

            }
            else
            {
                fullPath = fullPath + ProjectPath;
                DirectoryInfo dirInfo = new DirectoryInfo(fullPath);
                if(dirInfo != null)
                    GetFolderProjectStructure(dirInfo, null);

            }


        }

        public void GetFolderProjectStructure(DirectoryInfo dirInfo,ProjectFolder parentFolder)
        {
            foreach (DirectoryInfo temp in dirInfo.GetDirectories())
            {
                ProjectFolder folder = new ProjectFolder();
                folder.Name = temp.Name;
                folder.Parent = parentFolder;
                if (parentFolder == null)
                {
                    this.ProjectFolders.Add(folder);
                }
                else
                {
                    parentFolder.Folders.Add(folder);
                }
                GetFolderProjectStructure(temp, folder);

            }
            foreach (FileInfo temp in dirInfo.GetFiles())
            {
                SourceCodeSchema.ProjectFiles file = new ProjectFiles();
                file.Name = temp.Name;
                file.Include = temp.FullName;
                if (parentFolder == null)
                    this.ProjectFiles.Add(file);
                else
                    parentFolder.Files.Add(file);
            }
        }


    }
}
