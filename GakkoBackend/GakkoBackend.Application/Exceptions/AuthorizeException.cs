using System;
using System.Collections.Generic;
using System.Text;

namespace GakkoBackend.Application.Exceptions
{
    public class AuthorizeException : Exception 
    {
        public AuthorizeException(string message)
      : base(message)
        {

        }
    }
}
