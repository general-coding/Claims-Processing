using claimsprocessing.api.Models;

namespace claimsprocessing.api.Services
{
    public interface IClaimService
    {
        Task<IEnumerable<tbl_claim>> GetClaimsAsync();
        
        Task<tbl_claim?> GetClaimByIdAsync(int id);
        
        Task<bool> CheckParentExistsAsync(int id);

        Task<tbl_claim?> CreateClaimAsync(tbl_claim user);
        
        Task<bool> UpdateClaimByIdAsync(int id, tbl_claim user);
        
        Task<bool> DeleteClaimByIdAsync(int id);
    }
}
