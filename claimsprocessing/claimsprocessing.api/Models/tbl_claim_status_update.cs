using System;
using System.Collections.Generic;

namespace claimsprocessing.api.Models;

public partial class tbl_claim_status_update
{
    public int claim_status_update_id { get; set; }

    public int claim_id { get; set; }

    public string claim_status { get; set; } = null!;

    public DateTime created_on { get; set; }

    public virtual tbl_claim claim { get; set; } = null!;
}
