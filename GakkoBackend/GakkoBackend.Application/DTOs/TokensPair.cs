using System;
using System.Collections.Generic;
using System.Text;

namespace GakkoBackend.Application.DTOs
{
    public class TokensPair
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
