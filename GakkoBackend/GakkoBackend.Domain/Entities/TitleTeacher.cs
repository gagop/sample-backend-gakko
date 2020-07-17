using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class TitleTeacher
    {
        public Guid IdTitle { get; set; }
        public Guid IdTeacher { get; set; }
        public DateTime IssuedAt { get; set; }

        public virtual Teacher IdTeacherNavigation { get; set; }
        public virtual TitleDict IdTitleNavigation { get; set; }
    }
}
