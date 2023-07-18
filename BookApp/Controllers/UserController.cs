using BookApp.auth;
using BookApp.Errors;
using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BookApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private UserService _userService;
        private JwtUtil _jwtUtil;

        public UserController(UserService userService, JwtUtil jwtUtil)
        {
            _userService = userService;
            _jwtUtil = jwtUtil;

        }

     [HttpPost("signup")]
public async Task<IActionResult> SignUp(User user)
{
    var token = await _userService.CreateUser(user);

    // Build the response body
    var responseBody = new
    {
        Message = "User created successfully",
        Token = token
    };

    return Ok(responseBody);
}

      [HttpPost("login")]
public async Task<IActionResult> Login(User user)
{
    var token = await _userService.LoginUser(user.Email, user.Password);

    var responseBody = new
    {
        Message = "User Logged in successfully",
        Token = token
    };

    if (token == null) 
    {
        return BadRequest(new ApiResponse(400));
    }

    return Ok(responseBody);
}
    [Authorize]
   [HttpGet("getCurrentUser")]
   public async Task<IActionResult> GetCurrentUserAsync(){

    var user = await _userService.GetCurrentUser();
        var responseBody = new
    {
        Message = "Current user found successfully",
        user = user
    };
    return Ok(responseBody);

   }    


    }

    
}
