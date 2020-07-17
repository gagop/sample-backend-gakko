using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class Faculty
    {
        public Faculty()
        {
            Teacher = new HashSet<Teacher>();
        }

        public Guid IdFaculty { get; set; }
        public string Name { get; set; }
        public Guid IdChief { get; set; }

        public virtual Teacher IdChiefNavigation { get; set; }
        public virtual ICollection<Teacher> Teacher { get; set; }
    }
}
