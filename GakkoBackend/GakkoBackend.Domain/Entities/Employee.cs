using System;
using System.Collections.Generic;

namespace GakkoBackend.Domain
{
    public partial class Employee
    {
        public Employee()
        {
            News = new HashSet<News>();
        }

        public Guid IdEmployee { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpDate { get; set; }
        public string PasswordHash { get; set; }

        public virtual Person IdEmployeeNavigation { get; set; }
        public virtual ICollection<News> News { get; set; }
    }
}
