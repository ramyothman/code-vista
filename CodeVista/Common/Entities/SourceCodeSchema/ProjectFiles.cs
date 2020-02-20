using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities.SourceCodeSchema
{
    public class ProjectFiles : Entities.Entity
    {
        ProjectFiles _DependentUpon = null;
        public ProjectFiles DependentUpon
        {
            set { _DependentUpon = value; }
            get { return _DependentUpon; }
        }

        private string _Include;
        public string Include
        {
            get { return _Include; }
            set
            {
                _Include = value;
            }
        }
        
    }
}
