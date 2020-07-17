using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class LanguageDictTeacher
    {
        public Guid IdTeacher { get; set; }
        public Guid IdLanguage { get; set; }

        public virtual LanguageDict IdLanguageNavigation { get; set; }
        public virtual Teacher IdTeacherNavigation { get; set; }
    }
}
