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

        /// <summary>
        /// Create a FAQ
        /// </summary>
        /// <param name="data"></param>
        [HttpPost]
        public async Task<ActionResult<FAQVM>> Create([FromBody] FAQCreateVM data)
        {
            var result = await _faqService.Create(data);
            return Ok(result);
        }

        /// <summary>
        /// List All active FAQs
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<FAQVM>>> GetAll()
        {
            bool isDeleted = false;
            return Ok(await _faqService.GetAll(isDeleted));
        }

        /// <summary>
        /// Get a FAQ by its Id
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<FAQVM>> Get([FromRoute] Guid id)
        {
            return Ok(await _faqService.Get(id));
        }


        /// <summary>
        /// Update a FAQ
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<FAQVM>> Update([FromBody] FAQUpdateVM data)
        {
            var result = await _faqService.Update(data);
            return Ok(result);
        }

        /// <summary>
        /// Soft Delete -retrievable
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        [HttpPut]
        [Route("delete/{id}")]
        public async Task<ActionResult> SoftDelete([FromRoute] Guid id)
        {
            await _faqService.SoftDelete(id);
            return Ok();
        }

        /// <summary>
        /// View All Previously Deleted FAQ
        /// </summary>
        [HttpGet]
        [Route("delete")]
        public async Task<ActionResult<List<FAQVM>>> GetAllSoftDelete()
        {
            bool isDeleted = true;
            return Ok(await _faqService.GetAll(isDeleted));
        }
    }
}
