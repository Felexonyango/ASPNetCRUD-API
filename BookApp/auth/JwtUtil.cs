using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using BookApp.Context;
using BookApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
namespace BookApp.auth
{
    public class JwtUtil
    {
        private readonly IConfiguration _configuration;
        public JwtUtil(IConfiguration configuration)
        {

            _configuration = configuration;
        }
        public string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:key"]));
            var credetials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], null, expires: DateTime.Now.AddMinutes(1),
            signingCredentials: credetials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}