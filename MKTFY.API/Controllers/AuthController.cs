using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKTFY.Models.ViewModels.Auth;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.API.Controllers
{
   /// <summary>
   /// Controller for User Authorization Endpoint
   /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
        
    {
        private readonly IAuthService _authService;
        /// <summary>
        /// internal service
        /// </summary>
        /// <param name="authService"></param>
        public AuthController(IAuthService authService) {
            _authService = authService;
        }

        /// <summary>
        /// Exchange an Auth Code for an Access Token
        /// </summary>
        /// <param name="authCode"></param>   
        /// 
        [HttpPost("token")]
        public async Task<ActionResult<AuthResponseVM>> Token([FromQuery] string authCode)
        {
            // Exchange the token
            var result = await _authService.ExchangeToken(authCode);
            if (result == null)
                return BadRequest(new { message = "Unable to authorize access" });
            return Ok(result);
        }

    }
}
