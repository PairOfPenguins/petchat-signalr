using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using petchat.Data;
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
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public UserController(IUserRepository userRepository, IPasswordService passwordService, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDTO loginDTO)
        {
            var user = await _userRepository.GetByUsernameAsync(loginDTO.Username);
            if (user == null || !_passwordService.VerifyPasswordHash(loginDTO.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid username or password");
            }

            var token = _tokenService.GenerateToken(user);
            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return Ok(users.Select(c=>c.ToUserDto()));
        }
        [Authorize]
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
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserDTO userDTO, [FromServices] IPasswordService passwordService)
        {
            var normalizedUsername = userDTO.Username.Trim().ToLower();

            if (await _userRepository.UserExists(normalizedUsername))
            {
                return BadRequest("User already exists");
            }

            byte[] passwordHash, passwordSalt;
            passwordService.CreatePasswordHash(userDTO.Password, out passwordHash, out passwordSalt);

            var user = await _userRepository.CreateAsync(userDTO.ToUserFromCreate(passwordHash,passwordSalt));
            return Ok(user.ToUserDto());
        }
        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateUserDTO userDTO)
        {
            var user = await _userRepository.UpdateAsync(id, userDTO);
            if (user == null)
            {
                return NotFound("User does not exists");
            }
            return Ok(user.ToUserDto());
        }

        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var user = await _userRepository.DeleteAsync(id);
            if (user == null)
            {
                return NotFound("User does not exist");
            }
            return Ok(user.ToUserDto());
        }




    }
}
