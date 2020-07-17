using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class StudentStatusDict
    {
        public Guid IdStatus { get; set; }
        public Guid IdStudent { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual StatusDict IdStatusNavigation { get; set; }
        public virtual Student IdStudentNavigation { get; set; }
    }
}
