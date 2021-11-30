using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKTFY.Models.ViewModels.User;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.API.Controllers.AdminPanel
{
    /// <summary>
    /// Endpoints for User Related Actions
    /// </summary>
    [Route("api/admin/user")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class APUserController : ControllerBase
    {

        private readonly IUserService _userService;
        /// <summary>
        /// internal service 
        /// </summary>
        /// <param name="userService"></param>
        public APUserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Create a user profile, user must already exist in Auth0, phone number is a 10 digit string
        /// </summary>  
        [HttpPost("{userId}")]
        public async Task<ActionResult<UserVM>> Create([FromBody] UserCreateVM data, [FromRoute] string userId)
        {
            // Perform the update
            var result = await _userService.Create(data, userId);
            return Ok(result);
        }

        /// <summary>
        /// Update a User's profile using Admin Role
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>

        [HttpPut("{userId}")]
            public async Task<ActionResult<UserVM>> Update([FromBody] UserUpdateVM data,[FromRoute] string userId)
            {
                //get user id from header
                var result = await _userService.Update(data, userId);
                return Ok(result);
            }


            /// <summary>
            /// Get a User's profile info
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            [HttpGet("{id}")]
            public async Task<ActionResult<UserVM>> Get([FromRoute] String id) 
            {
                //set the userId
                return Ok(await _userService.Get(id, id));

            }

        }
}
