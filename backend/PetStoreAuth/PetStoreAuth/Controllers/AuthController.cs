using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetStoreAuth.Dtos;
using PetStoreAuth.Interfaces;

namespace PetStoreAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authservice) 
        {
            _authService = authservice;
        }


        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await _authService.RegisterUserAsync(user)) 
            {
                ModelState.AddModelError("Create Error", "Something went wrong while creating user or user already exists");
                return BadRequest(ModelState);
            }

            return StatusCode(201);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(UserDto user) 
        {
            if (!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }

            if (!await _authService.LoginUserAsync(user)) 
            {
                return BadRequest("Username and/or password are incorrect");
            }

            var jwt = _authService.GenerateJsonWebToken(user.Username);
            AuthResponseDto response = new AuthResponseDto() 
            {
                Authorization = jwt
            };

            return Ok(response);
        }
    }
}
