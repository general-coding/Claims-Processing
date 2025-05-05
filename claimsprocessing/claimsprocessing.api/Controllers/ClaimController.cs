using AutoMapper;
using claimsprocessing.api.DTO;
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
        private readonly IMapper _mapper;

        public ClaimController(IClaimService claimService, IMapper mapper)
        {
            _claimService = claimService;
            _mapper = mapper;
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

        // GET: api/Claim/ByUserId/5
        [HttpGet("ByUserId/{claimUserId}")]
        public async Task<ActionResult<IEnumerable<tbl_claim>>> GetClaimsByClaimUserId(int claimUserId)
        {
            IEnumerable<tbl_claim> claims = await _claimService.GetClaimsByClaimUserIdAsync(claimUserId);

            if (claims == null
                || (claims != null && !claims.Any()))
            {
                return NotFound();
            }

            return Ok(claims);
        }

        // PUT: api/Claim/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClaimById(int id, ClaimUpdateDTO claimUpdateDTO)
        {
            if (id != claimUpdateDTO.claim_id)
            {
                return BadRequest();
            }

            tbl_claim? claim = _mapper.Map<tbl_claim>(claimUpdateDTO);

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
        public async Task<ActionResult<tbl_user>> CreateClaim(ClaimCreateDTO claimCreateDTO)
        {
            if (claimCreateDTO == null)
            {
                return BadRequest();
            }
            if (!await _claimService.CheckClaimUserExistsAsync(claimCreateDTO.claim_user_id))
            {
                return BadRequest("User does not exist.");
            }

            tbl_claim? claim = _mapper.Map<tbl_claim>(claimCreateDTO);

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