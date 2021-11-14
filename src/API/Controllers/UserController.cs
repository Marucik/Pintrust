using System;
using System.Threading.Tasks;
using API.Domain;
using API.Domain.Interfaces;
using API.Dto;
using API.Repositories;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticateService _authenticateService;

        public UserController(ILogger<UserController> logger, IUserRepository userRepository, IAuthenticateService authenticateService)
        {
            _logger = logger;
            _userRepository = userRepository;
            _authenticateService = authenticateService;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> CreateUser(NewUserDto entity)
        {
            var user = new User(entity);
            await _userRepository.InsertAsync(user);
            return Ok();
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> Authenticate(AuthenticateRequest model)
        {
            var response = await _authenticateService.Authenticate(model);

            // _logger.LogInformation(model.ToString());

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                var userId = (String)Request.HttpContext.Items["UserId"];
                if (userId == null) return Unauthorized();

                var user = await _userRepository.GetById(Guid.Parse(userId));

                return Ok(new UserProfile(user));
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}