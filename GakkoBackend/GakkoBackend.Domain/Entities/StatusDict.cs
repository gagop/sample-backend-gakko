using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class StatusDict
    {
        public StatusDict()
        {
            StudentStatusDict = new HashSet<StudentStatusDict>();
        }

        public Guid IdStatus { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StudentStatusDict> StudentStatusDict { get; set; }
    }
}
