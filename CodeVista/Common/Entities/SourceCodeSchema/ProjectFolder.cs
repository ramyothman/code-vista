using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities.SourceCodeSchema
{
    public class ProjectFolder : Entities.Entity
    {
        private bool _SpecialFolder;
        public bool SpecialFolder
        {
            get { return _SpecialFolder; }
            set
            {
                _SpecialFolder = value;
            }
        }
        public ProjectFolder ParentFolder
        {
            set { _Parent = value; }
            get { return _Parent as ProjectFolder; }
        }


        private List<ProjectFolder> _Folders = new List<ProjectFolder>();
        public List<ProjectFolder> Folders
        {
            get { return _Folders; }
            set { _Folders = value; }
        }

        private List<ProjectFiles> _Files = new List<ProjectFiles>();
        public List<ProjectFiles> Files
        {
            get { return _Files; }
            set { _Files = value; }
        }

        public static ProjectFolder GetFolder(string folderName,List<ProjectFolder> folders,out bool Exists)
        {
            ProjectFolder folder = null;
            Exists = false;
            foreach (ProjectFolder folderCheck in folders)
            {
                if (folderCheck.Name.ToLower() == folderName.ToLower())
                {
                    Exists = true;
                    folder = folderCheck;
                    break;
                }
            }
            if (folder == null)
            {
                folder = new ProjectFolder();
                folder.Name = folderName;
            }
            return folder;
        }
    }
}
