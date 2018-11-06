using Odin.Core.Helper;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Odin.Infrastructure.Helpers
{
    public class SecureHashProvider : ISecureHashProvider
    {
        private const int SaltSize = 16;
        private const int HashSize = 20;

        public string Hash(string text)
        {
            return Hash(text, 10000);
        }

        public bool IsHashSupported(string hashString)
        {
            return hashString.Contains("$Some~Random~Text$");
        }
     
        public bool Verify(string text, string hashedText)
        {
            // Check hash
            if (!IsHashSupported(hashedText))
            {
                throw new NotSupportedException("The hashtype is not supported");
            }

            // Extract iteration and Base64 string
            var splittedHashString = hashedText.Replace("$Some~Random~Text$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            // Get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // Get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(text, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Get result
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
      
        private string Hash(string text, int iterations)
        {
            // Create salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            // Create hash
            var pbkdf2 = new Rfc2898DeriveBytes(text, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            // Combine salt and hash
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // Convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            // Format hash with extra information
            return string.Format("$Some~Random~Text${0}${1}", iterations, base64Hash);
        }      
    }
}
