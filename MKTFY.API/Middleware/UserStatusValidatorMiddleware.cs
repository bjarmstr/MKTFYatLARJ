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
using MKTFY.API.Helpers;

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
            //get the userId using the HttpContext &  UserHelpers GetId()  
            var userId = context.User.GetId();

            var validUser = await userRepository.CheckValidUser(userId);
            if (validUser == "blocked")
            {
                context.Response.StatusCode = 401; //UnAuthorized
                await context.Response.WriteAsync("Blocked User");
                return;
            }

            await _next.Invoke(context);

        }


    }
}

