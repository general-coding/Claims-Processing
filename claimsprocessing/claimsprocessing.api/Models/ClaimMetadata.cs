using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace claimsprocessing.api.Models
{
    public class ClaimMetadata
    {
        [ValidateNever]
        public virtual tbl_user claim_user { get; set; } = null!;
    }
}