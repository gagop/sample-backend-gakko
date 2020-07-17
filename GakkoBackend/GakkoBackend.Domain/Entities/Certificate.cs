using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class Certificate
    {
        public Guid IdCertificate { get; set; }
        public int IssuedAt { get; set; }
        public string Name { get; set; }
        public double Grade { get; set; }
        public string Level { get; set; }
        public Guid IdLanguage { get; set; }

        public virtual LanguageDict IdLanguageNavigation { get; set; }
    }
}
