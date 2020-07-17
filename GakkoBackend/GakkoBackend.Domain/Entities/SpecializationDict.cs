using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class SpecializationDict
    {
        public SpecializationDict()
        {
            StudiesMode = new HashSet<StudiesMode>();
        }

        public Guid IdSpecialization { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<StudiesMode> StudiesMode { get; set; }
    }
}
