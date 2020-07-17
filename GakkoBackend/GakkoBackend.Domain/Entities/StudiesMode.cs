using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class StudiesMode
    {
        public StudiesMode()
        {
            Curriculum = new HashSet<Curriculum>();
        }

        public Guid IdStudiesMode { get; set; }
        public int Name { get; set; }
        public Guid IdLanguage { get; set; }
        public Guid IdSpecialization { get; set; }

        public virtual LanguageDict IdLanguageNavigation { get; set; }
        public virtual SpecializationDict IdSpecializationNavigation { get; set; }
        public virtual ICollection<Curriculum> Curriculum { get; set; }
    }
}
