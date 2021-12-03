﻿using Microsoft.AspNetCore.Authorization;
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

namespace MKTFY.API.Controllers.AdminPanel
{
    [Route("api/admin/")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class APListingController : ControllerBase
    {
        private readonly IListingService _listingService;

        public APListingController(IListingService listingService)
        {
            _listingService = listingService;
        }

        /// <summary>
        /// Add a new Listing
        /// </summary>
        /// <remarks> Category Id 1 = Cars and Vehicles, Id 2 = Furniture, Id 3 = Electronics, Id 4 = Real Estate</remarks>
        [HttpPost("listing/{userId}")]
        public async Task<ActionResult<ListingVM>> Create([FromBody] ListingCreateVM data, [FromRoute] string userId)
        {
            var result = await _listingService.Create(data, userId);
            return Ok(result);
        }

        /// <summary>
        /// Get all Listings
        /// </summary>
        /// <returns></returns>
        [HttpGet("listing")]
        public async Task<ActionResult<List<ListingVM>>> GetAll()
        {
            return Ok(await _listingService.GetAll());
        }

        /// <summary>
        /// Update Listing
        /// </summary>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPut("listing/{userId}")]
        public async Task<ActionResult<ListingVM>> Update([FromBody] ListingUpdateVM data, [FromRoute] string userId)
        {
            
            var result = await _listingService.Update(data);
            return Ok(result);
        }

        /// <summary>
        /// Hard Delete.  Removes Listing from database.  For admin users only.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("listing/{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            await _listingService.Delete(id);
            // Return just the 200 response
            return Ok();
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
        /// valid status deleted, listed, pending, cancelled, sold
        /// </summary>
        [HttpPut("listing/{id}/{status}")]
        public async Task<ActionResult> ChangeTransactionStatus([FromRoute] Guid id, string status)
        {
            //check that only valid Transaction Statuses 
            string[] validStatus = { "listed", "deleted", "pending", "cancelled", "sold" };
            if (!validStatus.Contains(status))
            {
                return BadRequest(new { message = "invalid status" });
            }

            //TODO admin panel will need further logic to override buyerId if using this endpoint to override TransactionStatus
            string buyerId = "";
            
            if (status == "pending")
            {
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
        /// List of User's Listings - listed, pending or sold
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
    }
}
