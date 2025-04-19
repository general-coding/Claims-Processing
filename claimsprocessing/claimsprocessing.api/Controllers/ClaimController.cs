using claimsprocessing.api.Models;
using claimsprocessing.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace claimsprocessing.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimController : ControllerBase
    {
        private readonly IClaimService _claimService;

        public ClaimController(IClaimService claimService)
        {
            _claimService = claimService;
        }

        // GET: api/Claims
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<tbl_claim>>> GetClaims()
        {
            IEnumerable<tbl_claim> claims = await _claimService.GetClaimsAsync();
            return Ok(claims);
        }

        // GET: api/Claim/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tbl_claim>> GetClaimById(int id)
        {
            tbl_claim? tbl_claim = await _claimService.GetClaimByIdAsync(id);

            if (tbl_claim == null)
            {
                return NotFound();
            }

            return Ok(tbl_claim);
        }

        // PUT: api/Claim/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClaimById(int id, tbl_claim claim)
        {
            if (id != claim.claim_id)
            {
                return BadRequest();
            }

            bool isUpdated = await _claimService.UpdateClaimByIdAsync(id, claim);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Claim
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<tbl_user>> CreateClaim(tbl_claim? claim)
        {
            if (claim == null)
            {
                return BadRequest();
            }

            claim = await _claimService.CreateClaimAsync(claim);
            return CreatedAtAction("GetClaimById", new { id = claim?.claim_id }, claim);
        }

        // DELETE: api/Claim/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClaimById(int id)
        {
            bool isDeleted = await _claimService.DeleteClaimByIdAsync(id);

            return isDeleted ? NoContent() : NotFound();
        }
    }
}
