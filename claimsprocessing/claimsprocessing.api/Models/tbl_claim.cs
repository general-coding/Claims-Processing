namespace claimsprocessing.api.Models;

public partial class tbl_claim
{
    public int claim_id { get; set; }

    public int claim_user_id { get; set; }

    public string claim_type { get; set; } = null!;

    public decimal claim_amount { get; set; }

    public string claim_status { get; set; } = null!;

    public DateTime created_on { get; set; }

    public DateTime? modified_on { get; set; }

    public virtual tbl_user claim_user { get; set; } = null!;

    public virtual ICollection<tbl_claim_status_update> tbl_claim_status_update { get; set; } = new List<tbl_claim_status_update>();
}