using BookApp.auth;
using BookApp.Context;
using BookApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BookApp.Services
{
    public class UserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private ApplicationDbContext _dbContext;
        private JwtUtil _jwtUtil;
        public UserService(
            ApplicationDbContext Context,
             JwtUtil util,
         ILogger<UserService> logger, 
         IHttpContextAccessor httpContextAccessor
         )
        {
            _dbContext = Context;
            _jwtUtil = util;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<string> CreateUser(User user)
        {
            if (await CheckEmailExist(user.Email))
            {
                throw new Exception("Email already exists.");
            }

            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
            };

            await _dbContext.Users.AddAsync(newUser);
            await _dbContext.SaveChangesAsync();

            // Generate token for the newly signed up user
            var token = _jwtUtil.GenerateToken(newUser);
            return token;
        }

        private async Task<bool> CheckEmailExist(string email)
        {
            return await _dbContext.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task<string?> LoginUser(string email, string password)
        {
            var _user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            var isPasswordValid = _user != null && BCrypt.Net.BCrypt.Verify(password, _user.Password);

            if (_user != null && isPasswordValid)
            {
                var token = _jwtUtil.GenerateToken(_user);
                return token;
            }

            return null;
        }

        public User? GetById(int id)
        {
            return _dbContext.Users.FirstOrDefault(u => u.Id == id);

        }
         private int GetCurrentUserId()
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                throw new InvalidOperationException("User not authenticated.");
            }

            return int.Parse(userIdClaim.Value);
        }

        public async Task<User> GetCurrentUser()
        {
            var userId = GetCurrentUserId();
            var user = await _dbContext.Users.FindAsync(userId);

            return user;
        }




    }



}