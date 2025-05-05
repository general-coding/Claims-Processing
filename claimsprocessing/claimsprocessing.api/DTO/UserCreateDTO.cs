namespace claimsprocessing.api.DTO
{
    public class UserCreateDTO
    {
        public string user_fname { get; set; } = null!;

        public string? user_mname { get; set; }

        public string user_lname { get; set; } = null!;

        public string? user_fullname { get; set; }

        public string user_email { get; set; } = null!;

        public string user_password { get; set; } = null!;
    }
}