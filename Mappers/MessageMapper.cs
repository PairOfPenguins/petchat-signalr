using petchat.DTOs.MessageDTOs;
using petchat.Models;

namespace petchat.Mappers
{
    public static class MessageMapper
    {
        public static MessageDTO ToMessageDTO(this Message message)
        {
            return new MessageDTO
            {
                AssignedUserId = message.AssignedUserId,
                Content = message.Content,
                CreatedDate = message.CreatedDate,
                Id = message.Id,
            };
        }
    }
}
