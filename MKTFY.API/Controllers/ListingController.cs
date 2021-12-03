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
    [Route("api/")]
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
        [HttpPost("listing")]
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
        //[Authorize(Roles = "Admin")]
        [HttpGet("listing")]
        public async Task<ActionResult<List<ListingVM>>> GetAll()
        {
            return Ok(await _listingService.GetAll());
        }


        [HttpGet("listing/{id}")]
        public async Task<ActionResult<ListingVM>> Get([FromRoute] Guid id)
        {
            return Ok(await _listingService.Get(id));
        }

        /// <summary>
        /// A listing with the seller details & number of listings the seller has
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("listing/{id}/seller")]
        public async Task<ActionResult<ListingWithSellerVM>> GetListingWithSeller([FromRoute] Guid id)
        {
            return Ok(await _listingService.GetListingWithSeller(id));
        }

        /// <summary>
        /// Update Listing
        /// </summary>
        /// <param name="data"></param>
        [HttpPut("listing/{id}")]
        public async Task<ActionResult<ListingVM>> Update([FromBody] ListingUpdateVM data)
        {
            var result = await _listingService.Update(data);
            return Ok(result);
        }

        /// <summary>
        /// Hard Delete.  Removes Listing from database.  For admin users only.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpDelete("listing/{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _listingService.Delete(id);
            // Return just the 200 response
            return Ok();
        }

        /// <summary>
        /// Get Listings from a given category, include a region in query string
        /// </summary>
        /// <param name="categoryId"></param>
        /// <remarks> Category 1 = Cars & Vehicles, Id 2 = Furniture, Id 3 = Electronics, Id 4 = Real Estate</remarks>
        [HttpGet("listing/category/{categoryId}")]
        ///TODO  string region -- query or route?
        public async Task<ActionResult<List<ListingVM>>> GetByCategory([FromRoute] int categoryId, [FromQuery] string region)
        {
            if (region == null) return BadRequest(new { message = "region required" });
            string userId = User.GetId();
            var result = await _listingService.GetByCategory(categoryId, region, userId);
            return Ok(result);
        }

        /// <summary>
        /// Get Listings based on search history - Deals for you
        /// </summary>
        /// <param name="region"></param>
        /// <returns></returns>
        [HttpGet("listing/category/deals")]
        public async Task<ActionResult<List<ListingVM>>> GetDeals(string region)
        {
            string userId = User.GetId();
            var result = await _listingService.GetDeals(userId, region);
            return Ok(result);
        }


        [HttpGet("listing/search")]
        public async Task<ActionResult<List<ListingVM>>> GetBySearchTerm(string searchTerm, string region)
        {
            // User.IsInRole("Admin")
            var search = new SearchCreateVM();
            search.UserId = User.GetId();
            search.SearchTerm = searchTerm.ToLower();
            var result = await _listingService.GetBySearchTerm(search, region);
            return Ok(result);
        }

        [HttpGet("listing/{id}/pickup")]
        public async Task<ActionResult<ListingSellerVM>> GetPickupInfo(Guid id)
        {
            var result = await _listingService.GetPickupInfo(id);
            return Ok(result);
        }


        /// <summary>
        /// Set Listing Transaction Status
        /// valid status deleted, pending, cancelled (which reverts the listing back to listed), sold
        /// </summary>
        [HttpPut("listing/{id}/{status}")]
        public async Task<ActionResult> ChangeTransactionStatus([FromRoute] Guid id, string status)
        {
            //check that only valid Transaction Statuses 
            string[] validStatus = { "deleted", "pending", "cancelled", "sold" };
            if (!validStatus.Contains(status))
            {
                return BadRequest(new { message = "invalid status" });
            }

            string buyerId = null;

            if (status == "pending")
            {
                //TODO admin panel will need logic to override buyerId if using this endpoint to override TransactionStatus
                if (User.IsInRole("Admin"))
                {
                    return BadRequest(new { message = "invalid user" });
                }
                buyerId = User.GetId();
            }
            await _listingService.ChangeTransactionStatus(id, status, buyerId);
            return Ok();

        }

        [HttpGet]
        [Route("mypurchases")]
        public async Task<ActionResult<List<ListingPurchaseVM>>> GetMyPurchases()
        {
            //get user id from the Http request
            string userId = User.GetId();
            var results = await _listingService.GetMyPurchases(userId);
            return Ok(results);
        }


        /// <summary>
        /// List of User's Listings - choose between listed, pending or sold
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("mylisting/{status}")]
        public async Task<ActionResult<List<ListingSummaryVM>>> GetMyListings(string status)
        {
            //get user id from the Http request
            string userId = User.GetId();
            var results = await _listingService.GetMyListings(userId, status);
            return Ok(results);
        }

        /// <summary>
        /// List of User's Listings - listed, pending or sold
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("mylisting")]
        public async Task<ActionResult<List<ListingSummaryVM>>> GetAllMyListings()
        {
            //get user id from the Http request
            string userId = User.GetId();
            //"all" - listed, pending & sold (not deleted)
            var results = await _listingService.GetAllMyListings(userId);
            return Ok(results);
        }
    }
}
