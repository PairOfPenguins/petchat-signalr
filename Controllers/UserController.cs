using Microsoft.AspNetCore.Mvc;
using petchat.DTOs.UserDTOs;
using petchat.Interfaces;
using petchat.Mappers;
using petchat.Models;

namespace petchat.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users.Select(c=>c.ToUserDto()));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var users = await _userRepository.GetByIdAsync(id);
            
            if (users == null)
            {
                return NotFound("User does not exist");
            }
            return Ok(users.ToUserDto());

        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserDTO userDTO)
        {
            var user = await _userRepository.CreateAsync(userDTO.ToUserFromCreate());
            return Ok(user.ToUserDto());
        }

    }
}
