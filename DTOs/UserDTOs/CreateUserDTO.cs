using System.ComponentModel.DataAnnotations;

namespace petchat.DTOs.UserDTOs
{
    public class CreateUserDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "Username cannot be less than 3 characters")]
        [MaxLength(30, ErrorMessage = "Username cannot be over 30 characters")]
        public string Username { get; set; }
    }
}
