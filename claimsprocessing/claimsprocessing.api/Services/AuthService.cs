using claimsprocessing.api.Models;
using claimsprocessing.api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace claimsprocessing.api.Services
{
    public class AuthService : IAuthService
    {
        private readonly claims_processingContext _context;

        public AuthService(claims_processingContext context)
        {
            _context = context;
        }

        public async Task<AuthResult> AuthenticateAsync(string email, string password)
        {
            tbl_user? user = await _context.tbl_user.Where(u => u.user_email == email).FirstOrDefaultAsync();

            if (user != null
                && user.user_email == email
                && BCrypt.Net.BCrypt.Verify(password, user.user_password))
            {
                return new AuthResult
                {
                    Success = true,
                    User = new { Email = user.user_email, Name = user.user_fullname }
                };
            }

            return new AuthResult
            {
                Success = false,
                Message = "Invalid email or password."
            };
        }
    }
}
