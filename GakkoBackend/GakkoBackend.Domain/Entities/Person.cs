using System;
using System.Collections.Generic;

namespace GakkoBackend.Entities
{
    public partial class Person
    {
        public Guid IdPerson { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string UniversityEmail { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string PayoutAccount { get; set; }
        public string Salt { get; set; }
        public string PasswordHash { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExp { get; set; }
        public Guid IdCountry { get; set; }

        public virtual CountryDict IdCountryNavigation { get; set; }
        public virtual Student Student { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
