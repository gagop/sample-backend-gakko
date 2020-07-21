using System;
using System.Security.Cryptography;

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
