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
        /// Update Listing
        /// </summary>
        /// <param name="data"></param>
        [HttpPut]
        public async Task<ActionResult<ListingVM>> Update([FromBody] ListingUpdateVM data)
        {
            var result = await _listingService.Update(data);
            return Ok(result);
        }

        [HttpDelete("listing/{id}")]
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
        [HttpGet("listing/category/{categoryId}")]
        public async Task<ActionResult<List<ListingVM>>> GetByCategory([FromRoute] int categoryId, string region)
        {
            var result = await _listingService.GetByCategory(categoryId, region);
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
        /// valid status pending, cancelled, sold
        /// </summary>
        [HttpPut("listing/{id}/{status}")]
        public async Task<ActionResult> ChangeTransactionStatus([FromRoute] Guid id, string status)
        {
            //TODO @@@jma validate status
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
