using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class Curriculum
    {
        public Curriculum()
        {
            StudentGroup = new HashSet<StudentGroup>();
            SubjectCurriculum = new HashSet<SubjectCurriculum>();
        }

        public Guid IdCurriculum { get; set; }
        public Guid IdStudiesMode { get; set; }
        public Guid IdSemestr { get; set; }

        public virtual Semester IdSemestrNavigation { get; set; }
        public virtual StudiesMode IdStudiesModeNavigation { get; set; }
        public virtual ICollection<StudentGroup> StudentGroup { get; set; }
        public virtual ICollection<SubjectCurriculum> SubjectCurriculum { get; set; }
    }
}
