using GakkoBackend.Application.DTOs.Responses;
using GakkoBackend.Entities;
using GakkoBackend.Shared.Constants;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GakkoBackend.Application.Account
{
    public static class Helpers
    {
        public static bool IsRefreshTokenExpired(DateTime refreshTokenExpDate)
        {
            return refreshTokenExpDate < DateTime.UtcNow;
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }

}
