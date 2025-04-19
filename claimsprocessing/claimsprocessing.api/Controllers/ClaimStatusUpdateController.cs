using claimsprocessing.api.Models;
using claimsprocessing.api.Services;
using Microsoft.AspNetCore.Mvc;

namespace claimsprocessing.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClaimStatusUpdateController : ControllerBase
    {
        private readonly IClaimStatusUpdateService _claimStatusUpdateService;

        public ClaimStatusUpdateController(IClaimStatusUpdateService claimStatusUpdateService)
        {
            _claimStatusUpdateService = claimStatusUpdateService;
        }

        // GET: api/ClaimStatusUpdate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tbl_claim_status_update>>> Gettbl_claim_status_update()
        {
            IEnumerable<tbl_claim_status_update> claimStatusUpdates = await _claimStatusUpdateService.GetClaimStatusUpdatesAsync();
            return Ok(claimStatusUpdates);
        }

        // GET: api/ClaimStatusUpdate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<tbl_claim_status_update>> Gettbl_claim_status_update(int id)
        {
            tbl_claim_status_update? tbl_claim_status_update = await _claimStatusUpdateService.GetClaimStatusUpdateByIdAsync(id);

            if (tbl_claim_status_update == null)
            {
                return NotFound();
            }

            return Ok(tbl_claim_status_update);
        }
    }
}
