using petchat.DTOs.MessageDTOs;

namespace petchat.DTOs.UserDTOs
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public DateTime CreatedDate { get; set; }
        public List<MessageDTO?> Messages { get; set; }
    }
}
