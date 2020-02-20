using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Entities.SourceCodeSchema
{
    public class ProjectReference
    {
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
