using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKTFY.Models.ViewModels.Listing;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class ListingController : ControllerBase
    {
        private readonly IListingService _listingService;

        public ListingController(IListingService listingService)
        {
            _listingService = listingService;
        }

        /// <summary>
        /// Add a new Listing
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<ActionResult<ListingVM>> Create([FromBody] ListingCreateVM data)
        {
            var result = await _listingService.Create(data);
            return Ok(result);

        }

        /// <summary>
        /// Get all Listings
        /// </summary>
        /// <returns></returns>
        /// <remarks> Category  Id 1 = Deals, Id 2 = Cars & Vehicles, Id 3 = Furniture, Id 4 = Electronics, Id 5 = Real Estate</remarks>

        [HttpGet]
        public async Task<ActionResult<List<ListingVM>>> GetAll()
        {

            return Ok(await _listingService.GetAll());

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ListingVM>> Get([FromRoute] Guid id)
        {
            return Ok(await _listingService.Get(id));

        }

        [HttpPut]

        public async Task<ActionResult<ListingVM>> Update([FromBody] ListingUpdateVM data)
        {
            var result = await _listingService.Update(data);
            return Ok(result);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _listingService.Delete(id);
            // Return a 200 response
            return Ok();

        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<List<ListingVM>>> GetByCategory([FromRoute]int categoryId)
        {

            var result = await _listingService.GetByCategory(categoryId);
            return Ok(result);

        }


    }
}
