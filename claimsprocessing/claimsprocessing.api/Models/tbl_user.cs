namespace claimsprocessing.api.Models;

public partial class tbl_user
{
    public int user_id { get; set; }

    public string user_fname { get; set; } = null!;

    public string? user_mname { get; set; }

    public string user_lname { get; set; } = null!;

    public string? user_fullname { get; set; }

    public string user_email { get; set; } = null!;

    public string user_password { get; set; } = null!;

    public DateTime created_on { get; set; }

    public DateTime? modified_on { get; set; }

    public virtual ICollection<tbl_claim> tbl_claim { get; set; } = new List<tbl_claim>();

    public virtual ICollection<tbl_claim_status_update> tbl_claim_status_update { get; set; } = new List<tbl_claim_status_update>();
}