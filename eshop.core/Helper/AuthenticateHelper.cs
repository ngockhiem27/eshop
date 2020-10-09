using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Text;

namespace eshop.core.Helper
{
    public class AuthenticateHelper
    {
        public static string HashPassword(string password)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.ASCII.GetBytes("eshop-secret"),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
            return hashed;
        }
    }
}
