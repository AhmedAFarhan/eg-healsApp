using System.ComponentModel.DataAnnotations;

namespace EGHeals.Models.Dtos.Users
{
    public class LoginUserRequestDto
    {
        [Required(ErrorMessage = "Error")]
        public string Username { get; set; } = default!;

        [Required(ErrorMessage = "Error")]
        public string Password { get; set; } = default!;
    }
}
