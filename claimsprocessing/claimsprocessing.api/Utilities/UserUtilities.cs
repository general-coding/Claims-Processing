using claimsprocessing.api.Models;

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
            string hash = BCrypt.Net.BCrypt.HashPassword(password);

            return hash;
        }
    }
}