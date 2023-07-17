using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using BCrypt.Net;
using System.Threading.Tasks;
using BookApp.auth;
using BookApp.Context;
using BookApp.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookApp.Services
{
    public class UserService
    {
         private readonly ILogger<UserService> _logger;
        private ApplicationDbContext _dbContext;
        private  JwtUtil  _jwtUtil;
        public UserService(ApplicationDbContext Context, JwtUtil util,ILogger<UserService>logger)
        {
            _dbContext = Context;
            _jwtUtil = util;
              _logger = logger;
        }


  public  string  CreateUser(User user){
    // if(checkEmailExist(user.Email)){}
    var newUser = new User
    {
        Name = user.Name,
        Email = user.Email,
         Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
    };
    

    _dbContext.Users.Add(newUser);
    _dbContext.SaveChanges();

    // Generate token for the newly signed up user
    var token = _jwtUtil.GenerateToken(newUser);
    return token;

   
  }

    public string LoginUser(string email, string password)
   {
            var _user = _dbContext.Users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());

        var isPasswordValid = BCrypt.Net.BCrypt.Verify(password, _user.Password);

            if (_user != null && isPasswordValid)
            {
                var token = _jwtUtil.GenerateToken(_user);

                return token;

            }
            return null;


        }    
       public User? GetById(int id){
        return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        
    }
       
    }

}