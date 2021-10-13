using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using API.Dto;
using API.Extensions;
using API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class PostController : ControllerBase
  {
    private readonly ILogger<PostController> _logger;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;

    public PostController(ILogger<PostController> logger, IPostRepository postRepository, IUserRepository userRepository)
    {
      _logger = logger;
      _postRepository = postRepository;
      _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Post>> GetAllPosts()
    {
      return await _postRepository.GetAllAsync();
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreatePost([FromForm] NewPostDto entity, IFormFile image)
    {
      try
      {
        var user = (User)Request.HttpContext.Items["User"];
        if (user == null) return Unauthorized();

        var imageUrl = await image.SaveFileAndGetUrl();
        var post = new Post(entity, imageUrl, user);
        await _postRepository.InsertAsync(post);
      }
      catch (Exception)
      {
        throw;
      }

      return Ok();
    }

    [HttpPost("{postId}/favourite")]
    public async Task<IActionResult> AddFavourite(Guid postId)
    {
      try
      {
        var user = (User)Request.HttpContext.Items["User"];
        if (user == null) return Unauthorized();

        await _userRepository.AddFavourite(user, postId);

      }
      catch (Exception)
      {
        throw;
      }

      return Ok();
    }
  }
}
