using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class StudentGroup
    {
        public StudentGroup()
        {
            StudentGroupStudent = new HashSet<StudentGroupStudent>();
        }

        public Guid IdStudentGroup { get; set; }
        public Guid IdCurriculum { get; set; }
        public string GroupNumber { get; set; }

        public virtual Curriculum IdCurriculumNavigation { get; set; }
        public virtual ICollection<StudentGroupStudent> StudentGroupStudent { get; set; }
    }
}
