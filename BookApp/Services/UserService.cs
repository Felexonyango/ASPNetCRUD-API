using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
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
        private ApplicationDbContext _dbContext;
        private  JwtUtil  _jwtUtil;
        public UserService(ApplicationDbContext Context, JwtUtil util)
        {
            _dbContext = Context;
            _jwtUtil = util;
        }


  public  string  CreateUser(User user){
    var newUser = new User
    {
        Name = user.Name,
        Email = user.Email,
        Password = user.Password
    };

    _dbContext.Users.Add(newUser);
    _dbContext.SaveChanges();

    // Generate token for the newly signed up user
    var token = _jwtUtil.GenerateToken(newUser);
    return token;

   
  }

    // Return the response with the token and body
 

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
   
       public User? GetById(int id){
        return _dbContext.Users.FirstOrDefault(u => u.Id == id);
        
    }
    }

}