using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class Student
    {
        public Student()
        {
            StudentGroupStudent = new HashSet<StudentGroupStudent>();
            StudentStatusDict = new HashSet<StudentStatusDict>();
        }

        public Guid IdStudent { get; set; }
        public int YearOfStudies { get; set; }
        public string IndexNumber { get; set; }

        public virtual Person IdStudentNavigation { get; set; }
        public virtual ICollection<StudentGroupStudent> StudentGroupStudent { get; set; }
        public virtual ICollection<StudentStatusDict> StudentStatusDict { get; set; }
    }
}
