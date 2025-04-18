using claimsprocessing.api.Models;
using Microsoft.EntityFrameworkCore;

namespace claimsprocessing.api.Services
{
    public class UserService : IUserService
    {
        private readonly claims_processingContext _context;

        public UserService(claims_processingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<tbl_user>> GetUsersAsync()
        {
            return await _context.tbl_user.ToListAsync();
        }

        public async Task<tbl_user?> GetUserByIdAsync(int id)
        {
            tbl_user? tbl_user = await _context.tbl_user.FindAsync(id);

            return tbl_user;
        }

        public async Task<tbl_user?> CreateUserAsync(tbl_user user)
        {
            _context.tbl_user.Add(user);

            try
            {
                await _context.SaveChangesAsync();

                return user;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        public async Task<bool> UpdateUserByIdAsync(int id, tbl_user user)
        {
            user.modified_on = DateTime.Now;

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUserByIdAsync(int id)
        {
            tbl_user? tbl_user = await _context.tbl_user.FindAsync(id);
            if (tbl_user == null)
            {
                return false;
            }

            _context.tbl_user.Remove(tbl_user);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
