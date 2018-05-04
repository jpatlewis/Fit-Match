using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace Capstone.Web.Helpers
{
    public class Security
    {
        public static byte[] GenerateSalt(int length)
        {
            byte[] salt = new byte[length];

            using (RNGCryptoServiceProvider Gen = new RNGCryptoServiceProvider())
            {
                Gen.GetBytes(salt);
            }

            return salt;
        }

        public static string Hash(string password, byte[] salt)
        {
            // Creates the crypto service provider and provides the salt - usually used to check for a password match
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, salt);

            byte[] hash = rfc2898DeriveBytes.GetBytes(20);      //gets the hased password

            return Convert.ToBase64String(hash);
        }
    }
}