using System.ComponentModel.DataAnnotations;

namespace claimsprocessing.api.DTO
{
    public class LoginRequestDTO
    {
        [Required]
        [EmailAddress]
        public string user_email { get; set; }

        [Required]
        public string user_password { get; set; }
    }
}
