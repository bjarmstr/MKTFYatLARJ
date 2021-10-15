using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKTFY.Models.ViewModels.User;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Create a user profile, user must already exist in Auth0
        /// </summary>  
        [HttpPost]
        public async Task<ActionResult<UserVM>> Create([FromBody] UserCreateVM data)
        {
          
            // Get the Auth Token from the header
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            // Perform the update
            var result = await _userService.Create(data, accessToken);
            //an error message is returned or if all is good then null is returned
            if (result != null)
                return BadRequest(new { message = "Unable to update the user profile" });

            return Ok();
        }
    }
}
