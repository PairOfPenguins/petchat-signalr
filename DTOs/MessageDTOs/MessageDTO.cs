using petchat.Models;

namespace petchat.DTOs.MessageDTOs
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int AssignedUserId { get; set; }
    }
}
