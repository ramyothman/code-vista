using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Entities.SourceCodeSchema
{
    public enum VisualStudioVersion
    {
        NotSet,
        VisualStudio2005,
        VisualStudio2008
    }
    public class SolutionFile : Entities.Entity
    {
        private VisualStudioVersion _Version = VisualStudioVersion.NotSet;
        public VisualStudioVersion Version
        {
            set { _Version = value; }
            get 
            {
                if (_Version == VisualStudioVersion.NotSet && !string.IsNullOrEmpty(SolutionCode))
                {
                    Regex reg = new Regex(@"# Visual Studio 2005", RegexOptions.Singleline);
                    Match match = reg.Match(SolutionCode);
                    if (match.Success)
                        _Version = VisualStudioVersion.VisualStudio2005;
                    reg = new Regex(@"# Visual Studio 2008", RegexOptions.Singleline);
                    match = reg.Match(SolutionCode);
                    if (match.Success)
                        _Version = VisualStudioVersion.VisualStudio2008;
                    
                    
                }

                return _Version; 
            }
        }

        private List<VisualStudioProject> _VisualStudioProjects = null;
        public List<VisualStudioProject> VisualStudioProjects
        {
            set { _VisualStudioProjects = value; }
            get
            {
                if (_VisualStudioProjects == null && !string.IsNullOrEmpty(this.SolutionCode))
                {
                    if(_VisualStudioProjects != null)
                        _VisualStudioProjects.Clear();  
                    Regex reg = new Regex(@"(Project\s*\(.*?EndProject)", RegexOptions.Singleline);
                    Match match;// reg.Match(SolutionCode);
                    for (match = reg.Match(SolutionCode); match.Success; match = match.NextMatch())
                    {
                        string []values = match.Value.Split('\n');
                        
                        string guidMatch = @"(\{{0,1}([0-9a-fA-F]){8}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){4}-([0-9a-fA-F]){12}\}{0,1})";
                        Regex internalReg = new Regex(@"\(""" + guidMatch + @"""\s*\)\s*=", RegexOptions.Singleline);
                        VisualStudioProject project = new VisualStudioProject();
                        #region Main Properties
                        Match internalMatch = internalReg.Match(values[0]);
                        if (internalMatch.Success)
                        {
                            values[0] = values[0].Replace(internalMatch.Value, "");
                            project.ProjectTypeGUID = internalMatch.Value;
                        }
                        internalReg = new Regex(@""".*?""", RegexOptions.Singleline);
                        int count = 0;
                        for (internalMatch = internalReg.Match(values[0]); internalMatch.Success; internalMatch = internalMatch.NextMatch())
                        {
                            switch (count)
                            {
                                case 0:
                                    project.Name = internalMatch.Value.Replace("\"","");
                                    break;
                                case 1:
                                    project.ProjectPath = internalMatch.Value.Replace("\"", "");
                                    break;
                                case 2:
                                    project.ProjectGuid = internalMatch.Value.Replace("\"", "");
                                    break;
                            }
                            count++;
                        }
                        #endregion

                        #region Extra Properties
                        foreach (string str in values)
                        {
                            if (str.Contains("TargetFramework"))
                            {
                                internalReg = new Regex(@""".*?""", RegexOptions.Singleline);
                                internalMatch = internalReg.Match(str);
                                if(internalMatch.Success)
                                    project.TargetFramework = internalMatch.Value.Replace("\"", "");
                            }
                            else if (str.Contains("VirtualPath"))
                            {
                                internalReg = new Regex(@""".*?""", RegexOptions.Singleline);
                                internalMatch = internalReg.Match(str);
                                if (internalMatch.Success)
                                {
                                    if(str.TrimStart().StartsWith("Debug"))
                                        project.DebugCompilerOptions.VirtualPath = internalMatch.Value.Replace("\"", "");
                                    else if(str.TrimStart().StartsWith("Release"))
                                        project.ReleaseCompilerOptions.VirtualPath = internalMatch.Value.Replace("\"", "");
                                }
                            }
                            else if (str.Contains("PhysicalPath"))
                            {
                                internalReg = new Regex(@""".*?""", RegexOptions.Singleline);
                                internalMatch = internalReg.Match(str);
                                if (internalMatch.Success)
                                {
                                    if (str.TrimStart().StartsWith("Debug"))
                                        project.DebugCompilerOptions.PhysicalPath = internalMatch.Value.Replace("\"", "");
                                    else if (str.TrimStart().StartsWith("Release"))
                                        project.ReleaseCompilerOptions.PhysicalPath = internalMatch.Value.Replace("\"", "");
                                }
                            }
                            else if (str.Contains("TargetPath"))
                            {
                                internalReg = new Regex(@""".*?""", RegexOptions.Singleline);
                                internalMatch = internalReg.Match(str);
                                if (internalMatch.Success)
                                {
                                    if (str.TrimStart().StartsWith("Debug"))
                                        project.DebugCompilerOptions.TargetPath = internalMatch.Value.Replace("\"", "");
                                    else if (str.TrimStart().StartsWith("Release"))
                                        project.ReleaseCompilerOptions.TargetPath = internalMatch.Value.Replace("\"", "");
                                }
                            }
                        }
                        #endregion

                        project.Parent = this;
                        project.LoadProject(SolutionPath);
                        if (_VisualStudioProjects == null)
                            _VisualStudioProjects = new List<VisualStudioProject>();
                        _VisualStudioProjects.Add(project);
                        
                    }
                }
                return _VisualStudioProjects;
            }
        }

        private string _SolutionPath;
        public string SolutionPath
        {
            set { _SolutionPath = value; }
            get { return _SolutionPath; }
        }

        private string _SolutionCode;
        public string SolutionCode
        {
            set { _SolutionCode = value; }
            get { return _SolutionCode; }
        }

        public void LoadSolution(string path)
        {
            SolutionPath = path;
            System.IO.FileInfo info = new System.IO.FileInfo(path);
            Name = info.Name;
            SolutionCode = System.IO.File.ReadAllText(path);
        }



    }
}
