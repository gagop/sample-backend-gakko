using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class Semester
    {
        public Semester()
        {
            Curriculum = new HashSet<Curriculum>();
        }

        public Guid IdSemestr { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<Curriculum> Curriculum { get; set; }
    }
}
