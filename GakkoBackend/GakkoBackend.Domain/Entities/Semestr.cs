using System;
using System.Collections.Generic;

namespace GakkoBackend.Domain
{
    public partial class Semestr
    {
        public Semestr()
        {
            Curriculum = new HashSet<Curriculum>();
        }

        public Guid IdSemestr { get; set; }
        public int Number { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual ICollection<Curriculum> Curriculum { get; set; }
    }
}
