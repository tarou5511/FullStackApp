using FullStackApp.Api.Services.Interfaces;
using FullStackApp.Api.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FullStackApp.Api.Models.DTOs;

namespace FullStackApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var user = await _userService.RegisterAsync(request.Username, request.Email, request.Password);
                var dto = _mapper.Map<UserDto>(user);
                return CreatedAtAction(nameof(GetById),new{id = dto.Id},dto);
            }catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
        
            var dto = _mapper.Map<UserDto>(user);
            return Ok(dto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            var dto = _mapper.Map<IEnumerable<UserDto>>(users);
            return Ok(dto);
        }
    }
}
