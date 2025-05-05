using claimsprocessing.api.Models;

namespace claimsprocessing.api.Services
{
    public interface IClaimStatusUpdateService
    {
        Task<IEnumerable<tbl_claim_status_update>> GetClaimStatusUpdatesAsync();

        Task<tbl_claim_status_update?> GetClaimStatusUpdateByIdAsync(int id);
    }
}