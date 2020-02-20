using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities.SourceCodeSchema
{
    public enum CompilerTypes
    {
        Debug,
        Release,
        Other
    }
    public class CompilerOptions : Entities.Entity
    {
        private CompilerTypes _CompilerType;
        public CompilerTypes CompilerType
        {
            set { _CompilerType = value; }
            get { return _CompilerType; }
        }

        private string _VirtualPath;
        public string VirtualPath
        {
            set { _VirtualPath = value; }
            get { return _VirtualPath; }
        }

        private string _PhysicalPath;
        public string PhysicalPath
        {
            set { _PhysicalPath = value; }
            get { return _PhysicalPath; }
        }

        private string _TargetPath;
        public string TargetPath
        {
            set { _TargetPath = value; }
            get { return _TargetPath; }
        }
    }
}
