using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKTFY.Models.ViewModels.Message;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService notificationService)
        {
            _messageService = notificationService;
        }

        //Create Message
        [HttpPost("message")]
        public async Task<ActionResult<MessageVM>> CreateMessage([FromBody] string message)
        {
            var result = await _messageService.CreateMessage(message);
            return Ok(result);
        }

    }
}
