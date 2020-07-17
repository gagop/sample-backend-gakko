using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class TitleDict
    {
        public TitleDict()
        {
            TitleTeacher = new HashSet<TitleTeacher>();
        }

        public Guid IdTitle { get; set; }
        public string Name { get; set; }
        public Guid IdCountry { get; set; }

        public virtual CountryDict IdCountryNavigation { get; set; }
        public virtual ICollection<TitleTeacher> TitleTeacher { get; set; }
    }
}
