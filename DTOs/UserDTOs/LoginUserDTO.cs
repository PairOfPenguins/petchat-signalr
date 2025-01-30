using System.ComponentModel.DataAnnotations;

namespace petchat.DTOs.UserDTOs
{
    public class LoginUserDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
