using petchat.DTOs.MessageDTOs;
using petchat.DTOs.UserDTOs;
using petchat.Models;

namespace petchat.Mappers
{
    public static class UserMapper
    {
        public static UserDTO ToUserDto(this User user)
        {
            return new UserDTO { Id = user.Id, Username = user.Username, Messages = user.Messages?.Select(c => c.ToMessageDTO()).ToList(), CreatedDate = user.CreatedDate };

        }
    }
}
