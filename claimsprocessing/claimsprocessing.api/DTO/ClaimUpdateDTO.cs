namespace claimsprocessing.api.DTO
{
    public class ClaimUpdateDTO
    {
        public int claim_id { get; set; }

        public int claim_user_id { get; set; }

        public string claim_type { get; set; } = null!;

        public decimal claim_amount { get; set; }

        public string claim_status { get; set; } = null!;

        public DateTime created_on { get; set; }
    }
}