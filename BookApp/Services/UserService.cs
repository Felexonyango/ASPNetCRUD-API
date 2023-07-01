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
  

    }

}