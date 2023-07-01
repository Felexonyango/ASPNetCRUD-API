using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApp.Services;

namespace BookApp.auth
{
    public class JwtMiddleware{
        
private readonly RequestDelegate _next;

    public JwtMiddleware(RequestDelegate next)
    {
        _next = next;
    }

        
    public async Task Invoke(HttpContext context, UserService userService, JwtUtil jwtUtils)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        var userId = jwtUtils.ValidateJwtToken(token);
        if (userId != null)
        {        
            
            context.Items["User"] = userService.GetById(userId.Value);
        }

        await _next(context);
    }
    
    }
}