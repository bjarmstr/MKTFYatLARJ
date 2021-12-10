using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKTFY.Models.ViewModels.Message;
using MKTFY.Models.ViewModels.Notifications;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost("notification/{userId}")]
        public async Task<ActionResult> Create([FromBody]NotificationCreateVM data)
        {
            await _notificationService.Create(data);
            return Ok();
        }

        [HttpGet("notification/{userId}")]
        public async Task<ActionResult<List<NotificationVM>>> Get(string userId)
        {
            var results = await _notificationService.Get(userId);
            return Ok(results);
        }

    

    }
}
