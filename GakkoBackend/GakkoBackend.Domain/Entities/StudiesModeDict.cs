using System;
using System.Collections.Generic;

namespace GakkoBackend.Domain
{
    public partial class StudiesModeDict
    {
        public StudiesModeDict()
        {
            Curriculum = new HashSet<Curriculum>();
        }

        public Guid IdStudiesMode { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Curriculum> Curriculum { get; set; }
    }
}
