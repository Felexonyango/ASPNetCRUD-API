
using BookApp.DTos;
using BookApp.Errors;
using BookApp.Models;
using BookApp.Services;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly ILogger<PostController> _logger;
        public PostService _PostService;
        public UserService _userService;
       
        public PostController(
            PostService postService,
         ILogger<PostController> logger,
         UserService userService)
        {
            _PostService = postService;
            _userService = userService;
            _logger = logger;
          

        }


        [HttpPost("create-post")]
        public async Task<ActionResult<Post>> AddPost(Post post)
        {
            var currentUser = await _userService.GetCurrentUser();

            _logger.LogInformation("Current User: {name}", currentUser.Name); // Log the currentUser
            if (currentUser == null)
            {
                return Unauthorized(new ApiResponse(401, "User not authenticated."));
            }

            await _PostService.AddPost(post, currentUser);

            return Ok();
        }




        [HttpGet("posts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPosts()
        {

            var allposts = await _PostService.GetAllPosts();

            return Ok(allposts);
        }
        [HttpGet("post/{id}")]
 public async Task<ActionResult<PostDto>> GetPost(int id)
{
    var post = await _PostService.GetPostById(id);

    if (post == null)
    {
        return NotFound(new ApiResponse(404));
    }

    var postDto = post.Adapt<PostDto>();

    var responseBody = new
    {
        message = "Successfully retrieved post",
        result = postDto
    };

    return Ok(responseBody);
}

        [HttpDelete("delete/{id}")]

        public IActionResult deletPostById(int id)
        {

            _PostService.deletePost(id);
            return Ok();
        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Post>> UpdatePostById([FromBody] Post post, int id)
        {

            if (post == null) return NotFound(new ApiResponse(404));
            await _PostService.UpdatePost(post, id);
            return Ok();
        }




    }
}