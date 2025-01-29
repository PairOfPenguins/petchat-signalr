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

        public static User ToUserFromCreate(this CreateUserDTO createUserDTO, byte[] PasswordHash, byte[] PasswordSalt)
        {
            return new User
            {
                Username = createUserDTO.Username,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt
            };
        }

        public static User ToUserFromUpdate(this UpdateUserDTO updateUserDTO)
        {
            return new User()
            {
                Username = updateUserDTO.Username
            };
        }
    }
}
