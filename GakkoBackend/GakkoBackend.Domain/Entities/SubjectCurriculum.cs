using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class SubjectCurriculum
    {
        public Guid IdSubject { get; set; }
        public Guid IdCurriculum { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual Curriculum IdCurriculumNavigation { get; set; }
        public virtual Subject IdSubjectNavigation { get; set; }
    }
}
