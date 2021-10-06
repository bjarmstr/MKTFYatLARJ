using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MKTFY.Models.ViewModels.FAQ;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FAQController : ControllerBase
    {
        private readonly IFAQService _faqService;

        public FAQController(IFAQService faqService)
        {
            _faqService = faqService;
        }

        [HttpPost]
        public async Task<ActionResult<FAQVM>> Create([FromBody] FAQCreateVM data)
        {
            var result = await _faqService.Create(data);
            return Ok(result);

        }

        [HttpGet]
        public async Task<ActionResult<List<FAQVM>>> GetAll()
        {

            return Ok(await _faqService.GetAll());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FAQVM>> Get([FromRoute] Guid id)
        {
            return Ok(await _faqService.Get(id));

        }

        [HttpPut]

        public async Task<ActionResult<FAQVM>> Update([FromBody] FAQUpdateVM data)
        {
            var result = await _faqService.Update(data);
            return Ok(result);

        }





    }
}
