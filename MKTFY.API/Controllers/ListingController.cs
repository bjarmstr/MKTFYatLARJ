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
    public class ListingController : ControllerBase
    {
        private readonly IListingService _listingService;

        public ListingController(IListingService listingService)
        {
            _listingService = listingService;
        }

        [HttpPost]
        public async Task<ActionResult<ListingVM>> Create([FromBody]ListingCreateVM data)
        {
            try
            {
                var result = await _listingService.Create(data);
                return Ok(result);

            }
            catch (DbUpdateException) {
                return StatusCode(StatusCodes.Status500InternalServerError, 
                    new { message = "Database Error" });
            
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
