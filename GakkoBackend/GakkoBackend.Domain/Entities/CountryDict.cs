using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class CountryDict
    {
        public CountryDict()
        {
            Person = new HashSet<Person>();
            TitleDict = new HashSet<TitleDict>();
        }

        public Guid IdCountry { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Person> Person { get; set; }
        public virtual ICollection<TitleDict> TitleDict { get; set; }
    }
}
