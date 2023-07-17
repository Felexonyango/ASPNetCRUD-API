using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Threading.Tasks;
using BookApp.auth;
using BookApp.Errors;
using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;


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
        public IActionResult SignUp(User user)
        {
            var token = _userService.CreateUser(user);

            // Build the response body
            var responseBody = new
            {
                Message = "User created successfully",
                Token = token
            };


            return Ok(responseBody);
        }

        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            var token = _userService.LoginUser(user.Email, user.Password);


            var responseBody = new
            {
                Message = "User Logged in  successfully",
                Token = token
            };


            if (token == null) return BadRequest(new ApiResponse(400));


            return Ok(responseBody);



        }
       


    }
}
