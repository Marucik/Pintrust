using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain;
using API.Domain.Interfaces;
using API.Dto;
using API.Extensions;
using API.Repositories;
using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostController(ILogger<PostController> logger, IPostRepository postRepository, IUserRepository userRepository, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _postRepository = postRepository;
            _userRepository = userRepository;
            _webHostEnvironment = webHostEnvironment;
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
                var userId = (String)Request.HttpContext.Items["UserId"];
                if (userId == null) return Unauthorized();

                var user = await _userRepository.GetById(Guid.Parse(userId));

                var imageUrl = await image.SaveFileAndGetUrl(_webHostEnvironment);
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
                var userId = (String)Request.HttpContext.Items["UserId"];
                if (userId == null) return Unauthorized();

                var user = await _userRepository.GetById(Guid.Parse(userId));

                await _userRepository.AddFavourite(user, postId);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpPost("{postId}/react")]
        public async Task<IActionResult> AddReaction(Guid postId, NewReactionDto userReaction)
        {
            try
            {
                var userId = (String)Request.HttpContext.Items["UserId"];
                if (userId == null) return Unauthorized();
                if (userReaction.reactionType != "like" && userReaction.reactionType != "dislike") return BadRequest("Unknown reaction type.");

                var user = await _userRepository.GetById(Guid.Parse(userId));

                await _userRepository.AddReaction(user, postId, userReaction.reactionType);
                await _postRepository.AddReaction(postId, userReaction.reactionType);

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

            return Ok();
        }
    }
}
