using System;
using System.Collections.Generic;
using System.Text;

namespace Common.SourceCodeExtraction
{
    public enum SourceCodeProjectType
    {
        VisualStudioSolution,
        VisualStudioProject,
        WebFolder
    }
    public class SourceExtractorManager
    {
        SourceCodeProjectType CodeProjectType = SourceCodeProjectType.VisualStudioSolution;
        public SourceExtractorManager()
        {

        }
        public SourceExtractorManager(string path)
        {
            LoadSourceProject(path);
        }

        public void LoadSourceProject(string path)
        {
            if (path.EndsWith(".sln"))
                CodeProjectType = SourceCodeProjectType.VisualStudioSolution;
            else if (path.EndsWith(".csproj") || path.EndsWith(".vbproj"))
                CodeProjectType = SourceCodeProjectType.VisualStudioProject;
            else
                CodeProjectType = SourceCodeProjectType.WebFolder;

            Entities.SourceCodeSchema.SolutionFile solution = new Common.Entities.SourceCodeSchema.SolutionFile();
            solution.LoadSolution(path);
            object x = solution.Version;
            object y = solution.VisualStudioProjects;
        }


    }
}
