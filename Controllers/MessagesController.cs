using Microsoft.AspNetCore.Mvc;
using petchat.DTOs.MessageDTOs;
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
        public MessageController(IMessageRepository messageRepository, IUserRepository userRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var messages = await _messageRepository.GetAllAsync();

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

            var message = await _messageRepository.CreateAsync(messageDTO.ToMessageFromCreate());
            return Ok(message.ToMessageDTO());
        }


    }
}
