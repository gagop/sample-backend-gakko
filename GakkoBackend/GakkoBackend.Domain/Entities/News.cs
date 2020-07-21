using System;
using System.Collections.Generic;

namespace GakkoBackend.Domain
{
    public partial class News
    {
        public Guid IdNews { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
        public Guid IdEmployee { get; set; }

        public virtual Employee IdEmployeeNavigation { get; set; }
    }
}
