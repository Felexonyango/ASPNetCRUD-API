using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookApp.auth;
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

        [AllowAnonymous]
        [HttpPost("create")]
        public IActionResult Login(User user)
        {
            IActionResult response = Unauthorized();
            
            var authenticatedUser = _userService.AuthenticateUser(user);
            
            if (authenticatedUser != null)
            {
                var token = _jwtUtil.GenerateToken(authenticatedUser);
                response = Ok(new { token });
            }
            
            return response;
        }
    }
}
