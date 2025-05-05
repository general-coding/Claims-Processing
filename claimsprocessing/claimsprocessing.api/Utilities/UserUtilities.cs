using claimsprocessing.api.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace claimsprocessing.api.Utilities
{
    public class UserUtilities
    {
        public static string GetUserFullName(tbl_user user)
        {
            if (!string.IsNullOrEmpty(user.user_mname))
            {
                return $"{user.user_fname} {user.user_mname} {user.user_lname}";
            }
            else
            {
                return $"{user.user_fname} {user.user_lname}";
            }
        }

        public static string GeneratePasswordHash(string password)
        {
            // Generate a 128-bit salt using a sequence of
            // cryptographically strong random bytes.
            // divide by 8 to convert bits to bytes
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(password: password!,
                                                                      salt: salt,
                                                                      prf: KeyDerivationPrf.HMACSHA256,
                                                                      iterationCount: 100000,
                                                                      numBytesRequested: 256 / 8));

            return hash;
        }
    }
}