using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class LanguageDict
    {
        public LanguageDict()
        {
            Certificate = new HashSet<Certificate>();
            LanguageDictTeacher = new HashSet<LanguageDictTeacher>();
            StudiesMode = new HashSet<StudiesMode>();
            Subject = new HashSet<Subject>();
        }

        public Guid IdLanguage { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Certificate> Certificate { get; set; }
        public virtual ICollection<LanguageDictTeacher> LanguageDictTeacher { get; set; }
        public virtual ICollection<StudiesMode> StudiesMode { get; set; }
        public virtual ICollection<Subject> Subject { get; set; }
    }
}
