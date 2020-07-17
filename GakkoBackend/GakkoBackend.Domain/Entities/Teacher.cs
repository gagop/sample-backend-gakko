using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class Teacher
    {
        public Teacher()
        {
            Faculty = new HashSet<Faculty>();
            LanguageDictTeacher = new HashSet<LanguageDictTeacher>();
            TeacherSubject = new HashSet<TeacherSubject>();
            TitleTeacher = new HashSet<TitleTeacher>();
        }

        public Guid IdTeacher { get; set; }
        public Guid IdFaculty { get; set; }

        public virtual Faculty IdFacultyNavigation { get; set; }
        public virtual Person IdTeacherNavigation { get; set; }
        public virtual ICollection<Faculty> Faculty { get; set; }
        public virtual ICollection<LanguageDictTeacher> LanguageDictTeacher { get; set; }
        public virtual ICollection<TeacherSubject> TeacherSubject { get; set; }
        public virtual ICollection<TitleTeacher> TitleTeacher { get; set; }
    }
}
