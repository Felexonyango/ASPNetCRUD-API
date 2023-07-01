using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookApp.Context;
using BookApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookApp.Services
{
    public class UserService
    {
        private ApplicationDbContext _dbContext;
        private readonly IConfiguration _configuration;
        public UserService(ApplicationDbContext Context, IConfiguration configuration)
        {
            _dbContext = Context;
            _configuration = configuration;
        }


        public User AuthenticateUser(User user)
        {
            User Iuser = null;
            if (user.Name == "admin" && user.Password == "1234")
            {
                Iuser = new User
                {
                    Name = "Felex",
                    Password = "12345"
                };
            }
            return user;
        }
        public string GenerateToken(User user) {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:key"]));
            var credetials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], null, expires: DateTime.Now.AddMinutes(1),
            signingCredentials: credetials);

             return new JwtSecurityTokenHandler().WriteToken(token);
             
        }


    }

}