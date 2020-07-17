using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class StudentGroupStudent
    {
        public Guid IdStudent { get; set; }
        public Guid IdStudentGroup { get; set; }
        public DateTime CreatedAt { get; set; }

        public virtual StudentGroup IdStudentGroupNavigation { get; set; }
        public virtual Student IdStudentNavigation { get; set; }
    }
}
