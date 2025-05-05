using AutoMapper;
using claimsprocessing.api.Models;
using claimsprocessing.api.Utilities;
using Microsoft.EntityFrameworkCore;

namespace claimsprocessing.api.Services
{
    public class UserService : IUserService
    {
        private readonly claims_processingContext _context;
        private readonly IMapper _mapper;

        public UserService(claims_processingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            user.user_fullname = UserUtilities.GetUserFullName(user);
            user.user_password = UserUtilities.GeneratePasswordHash(user.user_password!);

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
            tbl_user? existingUser = await _context.tbl_user.FindAsync(id);

            if (existingUser == null)
            {
                return false;
            }

            //Map online the updated fields from the input 'user' to the 'existingUser'
            _mapper.Map(user, existingUser);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

            //_context.Entry(user).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //    return true;
            //}
            //catch (Exception)
            //{
            //    return false;
            //}
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