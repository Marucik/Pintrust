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
  [Route("api/[controller]")]
  public class ProductController : ControllerBase
  {
    private readonly ILogger<ProductController> _logger;
    private readonly IPostRepository _postRepository;

    public ProductController(ILogger<ProductController> logger, IPostRepository postRepository)
    {
      _logger = logger;
      _postRepository = postRepository;
    }

    [HttpGet]
    public async Task<IEnumerable<Post>> GetAllProducts()
    {
      return await _postRepository.GetAllAsync();
    }

    [HttpPost]
    [Consumes("multipart/form-data")]

    public async Task<IActionResult> PostProduct([FromForm] NewPostDto entity, IFormFile image)
    {
      try
      {
        var imageUrl = await image.SaveFileAndGetUrl();
        var post = new Post(entity, imageUrl);
        await _postRepository.InsertAsync(post);
      }
      catch (System.Exception)
      {

        throw;
      }

      return Ok();
    }
  }
}
