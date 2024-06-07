using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dto.Account
{
    public class RegisterDto
    {
        [Required]
        public string? UserName { get; set; }
        [Required]
        [EmailAddress]
        public string? EmailAddress { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "There should be atleast 8 characters...")]
        [MaxLength(50, ErrorMessage = "The maximum length is 50 characters")]
        public string? Password { get; set; }
    }
}
