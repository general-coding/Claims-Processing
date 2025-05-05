using claimsprocessing.api.Models;
using Microsoft.EntityFrameworkCore;

namespace claimsprocessing.api.Services
{
    public class ClaimStatusUpdateService : IClaimStatusUpdateService
    {
        private readonly claims_processingContext _context;

        public ClaimStatusUpdateService(claims_processingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<tbl_claim_status_update>> GetClaimStatusUpdatesAsync()
        {
            return await _context.tbl_claim_status_update.ToListAsync();
        }

        public async Task<tbl_claim_status_update?> GetClaimStatusUpdateByIdAsync(int id)
        {
            tbl_claim_status_update? tbl_claim_status_update = await _context.tbl_claim_status_update.FindAsync(id);

            return tbl_claim_status_update;
        }
    }
}