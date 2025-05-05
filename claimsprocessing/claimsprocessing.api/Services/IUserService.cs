using claimsprocessing.api.Models;

namespace claimsprocessing.api.Services
{
    public interface IUserService
    {
        Task<IEnumerable<tbl_user>> GetUsersAsync();

        Task<tbl_user?> GetUserByIdAsync(int id);

        Task<tbl_user?> CreateUserAsync(tbl_user user);

        Task<bool> UpdateUserByIdAsync(int id, tbl_user user);

        Task<bool> DeleteUserByIdAsync(int id);
    }
}