using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKTFY.API.Helpers;
using MKTFY.Models.ViewModels;
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
    [Authorize]
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
        /// <remarks> Category Id 1 = Cars and Vehicles, Id 2 = Furniture, Id 3 = Electronics, Id 4 = Real Estate</remarks>
        [HttpPost]
        public async Task<ActionResult<ListingVM>> Create([FromBody] ListingCreateVM data)
        {
            var userId = User.GetId();
            var result = await _listingService.Create(data, userId);
            return Ok(result);
        }

        /// <summary>
        /// Get all Listings
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Update Listing
        /// </summary>
        /// <param name="data"></param>
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
            // Return just the 200 response
            return Ok();
        }

        /// <summary>
        /// Get Listings from a given category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <remarks> Category 1 = Cars & Vehicles, Id 2 = Furniture, Id 3 = Electronics, Id 4 = Real Estate</remarks>
        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<List<ListingVM>>> GetByCategory([FromRoute]int categoryId, string region)
        {
            var result = await _listingService.GetByCategory(categoryId, region);
            return Ok(result);
        }
        
        /// <summary>
        /// Get Listings based on search history - Deals for you
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        [HttpGet("category/deals")]
        public async Task<ActionResult<List<ListingVM>>> GetDeals(string region)
        {
            string userId = User.GetId();
            var result = await _listingService.GetDeals(userId, region);
            return Ok(result);
        }


        [HttpGet("search")]
        public async Task<ActionResult<List<ListingVM>>> GetBySearchTerm(string searchTerm , string region)
        {
            var search = new SearchCreateVM();
            search.UserId = User.GetId();
            search.SearchTerm = searchTerm.ToLower();
            var result = await _listingService.GetBySearchTerm(search, region);
            return Ok(result);
        }

        [HttpGet("{id}/pickup")]
        public async Task<ActionResult<ListingSellerVM>> GetPickupInfo(Guid id)
        {
            var result = await _listingService.GetPickupInfo(id);
            return Ok(result);
        }


        /// <summary>
        /// Set Listing Status to Pending
        /// </summary>
        [HttpPut("{id}/{status}")]
        public async Task<ActionResult> Pending([FromRoute] Guid id, string status)
        {
            await _listingService.Pending(id, status);
            return Ok();

            //TODO @@@JMA  don't list items in status other than listed
            //add Datesold to sold items 
        }








    }
}
