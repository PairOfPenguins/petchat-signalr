using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using petchat.Data;
using petchat.DTOs.MessageDTOs;
using petchat.Helpers;
using petchat.Interfaces;
using petchat.Mappers;
using petchat.Repositories;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] QueryObject query)
        {
            var messages = await _messageRepository.GetAllAsync(query);

            var messagesDTO = messages.Select(m => m.ToMessageDTO());

            return Ok(messagesDTO);
        }
        [Authorize]
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

        [Authorize] 
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateMessageDTO messageDTO)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token");
            }

            int userId = int.Parse(userIdClaim);

            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return BadRequest("Assigned User does not exist");
            }

            messageDTO.AssignedUserId = userId;

            var username = user.Username;

            var message = await _messageRepository.CreateAsync(messageDTO.ToMessageFromCreate(username));

            await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.ToMessageDTO());

            return Ok(message.ToMessageDTO());
        }

        [Authorize]
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

        [Authorize]
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
