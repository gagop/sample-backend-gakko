using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class TeacherSubject
    {
        public Guid IdTeacher { get; set; }
        public Guid IdSubject { get; set; }
        public int HourlyRate { get; set; }

        public virtual Subject IdSubjectNavigation { get; set; }
        public virtual Teacher IdTeacherNavigation { get; set; }
    }
}
