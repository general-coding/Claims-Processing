using System;
using System.Collections.Generic;

namespace claimsprocessing.api.Models;

public partial class tbl_user
{
    public int user_id { get; set; }

    public string user_fname { get; set; } = null!;

    public string? user_mname { get; set; }

    public string user_lname { get; set; } = null!;

    public string user_email { get; set; } = null!;

    public string user_password { get; set; } = null!;

    public DateTime created_on { get; set; }

    public DateTime? modified_on { get; set; }
}
