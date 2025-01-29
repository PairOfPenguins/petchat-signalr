using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using petchat.Data;
using petchat.DTOs.MessageDTOs;
using petchat.Helpers;
using petchat.Interfaces;
using petchat.Mappers;
using petchat.Repositories;

namespace petchat.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class MessageController: ControllerBase
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IHubContext<ChatHub> _hubContext;
        public MessageController(IMessageRepository messageRepository, IUserRepository userRepository, IHubContext<ChatHub> hubContext)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] QueryObject query)
        {
            var messages = await _messageRepository.GetAllAsync(query);

            var messagesDTO = messages.Select(m => m.ToMessageDTO());

            return Ok(messagesDTO);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var message = await _messageRepository.GetByIdAsync(id);
            if (message == null)
            {
                return NotFound("Message does not exist");
            }
            return Ok(message.ToMessageDTO());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMessageDTO messageDTO)
        {
            var user = await _userRepository.GetByIdAsync(messageDTO.AssignedUserId);
            if (user == null)
            {
                return BadRequest("Assigned User does not exist");
            }

            var username = user.Username;

            var message = await _messageRepository.CreateAsync(messageDTO.ToMessageFromCreate(username));

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.ToMessageDTO());

            return Ok(message.ToMessageDTO());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateMessageDTO messageDTO)
        {
            var message = await _messageRepository.UpdateAsync(id, messageDTO.ToMessageFromUpdate());
            if (message == null)
            {
                return NotFound("Message does not exist");
            }
            return Ok(message.ToMessageDTO());
        }

        [HttpDelete("{id:int}")]

        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var message = await _messageRepository.DeleteAsync(id);
            if (message == null)
            {
                return NotFound("Message does not exist");
            }
            return Ok(message.ToMessageDTO());
        }



    }
}
