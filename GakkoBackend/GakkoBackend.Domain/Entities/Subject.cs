using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class Subject
    {
        public Subject()
        {
            SubjectCurriculum = new HashSet<SubjectCurriculum>();
            TeacherSubject = new HashSet<TeacherSubject>();
        }

        public Guid IdSubject { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Ects { get; set; }
        public Guid IdLanguage { get; set; }

        public virtual LanguageDict IdLanguageNavigation { get; set; }
        public virtual ICollection<SubjectCurriculum> SubjectCurriculum { get; set; }
        public virtual ICollection<TeacherSubject> TeacherSubject { get; set; }
    }
}
