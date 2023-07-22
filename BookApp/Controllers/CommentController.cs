using BookApp.Errors;
using BookApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
  
     private readonly ILogger<CommentController> _logger;
        public CommentService  _commentService;
        public UserService _userService;

        public CommentController(
        CommentService commentService, 
         ILogger<CommentController> logger, 
         UserService userService
          )
        {
            _commentService = commentService;
            _userService = userService;
               _logger = logger;

        }


[HttpPost("create/{postId}")]
public async Task<ActionResult<Comment>> AddComment(int postId, Comment comment)
{
    var currentUser = await _userService.GetCurrentUser();

    if (currentUser == null)
    {
        return Unauthorized(new ApiResponse(401, "User not authenticated."));
    }

    await _commentService.AddComment(postId, comment, currentUser);

    return Ok();
}




     
        [HttpGet("comments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments()
        {

            var allcomments = await _commentService.GetAllComments();
            
            return Ok(allcomments);
        }
        [HttpGet("comment/{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var post = await _commentService.GetCommentById(id);
     
            if (post == null) return NotFound(new ApiResponse(404));
            var responseBody = new
            {
                message = "Successfully retrieved post",
                result = post
            };

            return Ok(responseBody);

        }

        [HttpDelete("delete/{id}")]

        public IActionResult deleteCommentById(int id)
        {

            _commentService.deleteComment(id);
            return Ok();
        }
        [HttpPut("update/{id}")]
        public async Task<ActionResult<Comment>> UpdatePostById([FromBody] Comment comment, int id)
        {

            if (comment == null) return NotFound(new ApiResponse(404));
            await _commentService.UpdateComment(comment, id);
            return Ok();
        }




    }
}