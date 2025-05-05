using AutoMapper;
using claimsprocessing.api.Models;
using Microsoft.EntityFrameworkCore;

namespace claimsprocessing.api.Services
{
    public class ClaimService : IClaimService
    {
        private readonly claims_processingContext _context;
        private readonly IMapper _mapper;

        public ClaimService(claims_processingContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<tbl_claim>> GetClaimsAsync()
        {
            return await _context.tbl_claim.ToListAsync();
        }

        public async Task<tbl_claim?> GetClaimByIdAsync(int id)
        {
            tbl_claim? tbl_claim = await _context.tbl_claim.FindAsync(id);

            return tbl_claim;
        }

        public async Task<IEnumerable<tbl_claim>> GetClaimsByClaimUserIdAsync(int claimUserId)
        {
            return await _context.tbl_claim.Where(c => c.claim_user_id == claimUserId)
                                           .ToListAsync();
        }

        public async Task<bool> CheckClaimUserExistsAsync(int userId)
        {
            return await _context.tbl_user.AnyAsync(e => e.user_id == userId);
        }

        public async Task<tbl_claim?> CreateClaimAsync(tbl_claim claim)
        {
            try
            {
                //Detach parent. Prevent EF Core from trying to insert a new user.
                claim.claim_user = null;

                _context.tbl_claim.Add(claim);

                await _context.SaveChangesAsync();

                return claim;
            }
            catch (DbUpdateException)
            {
                return null;
            }
        }

        public async Task<bool> UpdateClaimByIdAsync(int id, tbl_claim claim)
        {
            tbl_claim? existingClaim = await _context.tbl_claim.FindAsync(id);

            if (existingClaim == null)
            {
                return false;
            }

            //Map online the updated fields from the input 'user' to the 'existingUser'
            _mapper.Map(claim, existingClaim);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

            //_context.Entry(claim).State = EntityState.Modified;

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

        public async Task<bool> DeleteClaimByIdAsync(int id)
        {
            tbl_claim? tbl_claim = await _context.tbl_claim.FindAsync(id);
            if (tbl_claim == null)
            {
                return false;
            }

            _context.tbl_claim.Remove(tbl_claim);

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