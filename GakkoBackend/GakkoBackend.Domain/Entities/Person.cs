using System;
using System.Collections.Generic;

namespace GakkoBackend.Domain
{
    public partial class Person
    {
        public Guid IdPerson { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public byte[] Image { get; set; }
        public bool Gender { get; set; }

        public virtual Candidate Candidate { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
