using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MKTFY.Shared.Exceptions;
using Npgsql;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using MKTFY.Repositories.Repositories.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;

namespace MKTFY.API.Middleware
{
    public class UserStatusValidatorMiddleware
    {
        private readonly RequestDelegate _next;

        public UserStatusValidatorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserRepository userRepository)
        {
            //get the userId from the jwt token    
            //var jwtToken = context.Request.Headers.TryGetValue("Authorization", out var jwt);
            //var handler = new JwtSecurityTokenHandler();
            //var token = handler.ReadJwtToken(jwt);
            //var userId = token.Audiences.

            //var userId = context.User.Identity.Name;
            //HttpContext.Items.TryGetValue("Username", out value);
  

            var jwtValid = context.Request.Headers.TryGetValue("HeaderAuthorization", out var jwt);

            //string token = context.Request.Headers["Authorization"];
            //if (!string.IsNullOrEmpty(token))
            //{
            //    var tok = token.Replace("Bearer ", "");
            //    var jwttoken = new JwtSecurityTokenHandler().ReadJwtToken(tok);

            //    var jti = jwttoken.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;
              
            //}

            // if (!context.Request.Headers.Keys.Contains("user-key")

            //var validUser = await userRepository.CheckValidUser(userId);
            //if (validUser == false)
            //{
            //    context.Response.StatusCode = 401; //UnAuthorized
            //                                       //why did we change our global expection handler response to JSON/?
            //    await context.Response.WriteAsync("Blocked User");
            //    return;
            //}

            await _next.Invoke(context);

        }

        
    }
}

