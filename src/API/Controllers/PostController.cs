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

    public PostController(ILogger<PostController> logger, IPostRepository postRepository)
    {
      _logger = logger;
      _postRepository = postRepository;
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
        // Request.Headers.TryGetValue("Authentication", out var AuthenticationHeader);
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
  }
}
