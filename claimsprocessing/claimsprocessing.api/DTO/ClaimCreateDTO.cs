namespace claimsprocessing.api.DTO
{
    public class ClaimCreateDTO
    {
        public int claim_user_id { get; set; }

        public string claim_type { get; set; } = null!;

        public decimal claim_amount { get; set; }

        public string claim_status { get; set; } = null!;
    }
}