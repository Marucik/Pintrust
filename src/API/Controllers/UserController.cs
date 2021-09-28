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

    [HttpPost]
    public async Task<IActionResult> CreateUser(NewUserDto entity)
    {
      var user = new User(entity);
      await _userRepository.InsertAsync(user);
      return Ok();
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate(AuthenticateRequest model)
    {
      var response = await _authenticateService.Authenticate(model);

      if (response == null)
        return BadRequest(new { message = "Username or password is incorrect" });

      return Ok(response);
    }
  }
}