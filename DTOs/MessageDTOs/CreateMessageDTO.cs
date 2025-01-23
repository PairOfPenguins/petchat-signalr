using System.ComponentModel.DataAnnotations;

namespace petchat.DTOs.MessageDTOs
{
    public class CreateMessageDTO
    {
        [Required]
        [MaxLength(300, ErrorMessage = "Message cannot be over 300 characters")]
        public string Content { get; set; }
        [Required]
        public int AssignedUserId {  get; set; }
    }
}
