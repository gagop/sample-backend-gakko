using System;
using System.Collections.Generic;

namespace GakkoBackend.Domain
{
    public partial class Curriculum
    {
        public Curriculum()
        {
            Candidate = new HashSet<Candidate>();
        }

        public Guid IdCurriculum { get; set; }
        public Guid IdStudiesMode { get; set; }
        public Guid IdSemestr { get; set; }

        public virtual Semestr IdSemestrNavigation { get; set; }
        public virtual StudiesModeDict IdStudiesModeNavigation { get; set; }
        public virtual ICollection<Candidate> Candidate { get; set; }
    }
}
