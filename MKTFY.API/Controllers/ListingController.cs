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
        public async Task<ActionResult<ListingVM>> Create([FromBody] ListingCreateVM data)
        {
            try
            {
                var result = await _listingService.Create(data);
                return Ok(result);

            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Database Error" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<ListingVM>>> GetAll()
        {
            try
            {
                return (await _listingService.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });

            }

        }

        [HttpGet ("{id}")]
        public async Task<ActionResult<ListingVM>> Get([FromRoute]Guid id)
        {
            try
            {
                return await _listingService.Get(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = ex.Message });

            }
        }

        [HttpPut]

        public async Task<ActionResult<ListingVM>> Update([FromBody] ListingUpdateVM data)
        {
            try
            {
                var result = await _listingService.Update(data);
                return Ok(result);

            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { message = "Database Error" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] Guid id)
        {
            try
            {
                // Tell the repository to delete the requested Listing entity
                await _listingService.Delete(id);

                // Return a 200 response
                return Ok();
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Unable to contact the database" });
            }
            catch
            {
                return BadRequest(new { message = "Unable to delete the requested listing" });
            }
        }
    }
}
