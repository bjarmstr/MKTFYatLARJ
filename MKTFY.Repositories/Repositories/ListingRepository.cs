using Microsoft.EntityFrameworkCore;
using MKTFY.Models.Entities;
using MKTFY.Repositories.Repositories.Interfaces;
using MKTFY.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Repositories.Repositories
{
    public class ListingRepository : IListingRepository
    {
        private readonly ApplicationDbContext _context;

        public ListingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Listing> Create(Listing src)
        {
            _context.Add(src);
            await _context.SaveChangesAsync();
            return src;
        }


        public async Task<Listing> Get(Guid id)
        {
            var result = await _context.Listings
                //.Include("ListingUploads").Include("ListingUploads.Upload")
                .Include(e => e.ListingUploads).ThenInclude(e => e.Upload)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (result == null) throw new NotFoundException("The requested listing could not be found");
            return result;
        }

        public async Task<List<Listing>> GetAll()
        {
            var results = await _context.Listings
                .Include(e => e.ListingUploads).ThenInclude(e => e.Upload)
                .ToListAsync();
            return results;
        }

        public async Task<Listing> Update(Listing src)
        {
            //get version of listing saved in db 
            var result = await _context.Listings
                 .Include(e => e.ListingUploads).ThenInclude(e => e.Upload)
                 .FirstOrDefaultAsync(i => i.Id == src.Id);
            if (result == null) throw new NotFoundException("The requested listing could not be found");
       
            //create lists of UploadIds of the old and new versions, compare them for changes
            var listofResultUploadIds = result.ListingUploads.Select(e => e.UploadId).ToList();
            var listofSrcUploadIds = src.ListingUploads.Select(e => e.UploadId).ToList();
            var resultNotSrc = listofResultUploadIds.Except(listofSrcUploadIds).ToList();
            var srcNotResult = listofSrcUploadIds.Except(listofResultUploadIds).ToList();
         
            //exception for null image TODO@@@jma 

           //iterate over those that need deleteing
            foreach(var upload in result.ListingUploads)
            {
                if (resultNotSrc.Contains(upload.UploadId))
                {
                    //remove reference in listingUploads table
                    result.ListingUploads.Remove(upload);
                    //remove image from Uploads table
                    _context.Uploads.Remove(upload.Upload);
                }
            }
          
            //add any new images to listingUploads table
            foreach(var uploadId in srcNotResult)
            {
                var listingUpload = src.ListingUploads.First(i => i.UploadId == uploadId);
                //add to the listing table
                result.ListingUploads.Add(listingUpload);
            }

       
            result.Product = src.Product;
            result.Details = src.Details;
            result.Price = src.Price;
            result.CategoryId = src.CategoryId;
            result.Condition = src.Condition;
            result.Region = src.Region;
           //result.ListingUploads is created above first removing images and then adding any additional images
            //doesn't update DateCreated or TransactionStatus
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task Delete(Guid id)
        {
            var result = await _context.Listings
                .Include(e =>e.ListingUploads).ThenInclude(e=>e.Upload)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (result == null) throw new NotFoundException("The requested listing could not be found");

            //delete uploads
           // var x = await _context.Uploads
            //    .Include(e=>e.Upload)
            //    .Where(upload => upload.Id == ListingUploads.UploadId)
            //    .Where(i => i.ListingId == id);

            //delete
            _context.Remove(result);
            await _context.SaveChangesAsync();
            
        }

        public async Task<List<Listing>> GetByCategory(int categoryId, string region)
        {
            var results = await _context.Listings
                .Where(listing => listing.CategoryId == categoryId && listing.Region == region)
                .Include(e => e.ListingUploads).ThenInclude(e => e.Upload)
                .ToListAsync();
            return results;
        }

        public async Task<List<Listing>> GetBySearchTerm(string searchTermLowerCase, string region)
        {
            var results = await _context.Listings
                .Where(listing => listing.Region == region &&
                   (listing.Details.ToLower().Contains(searchTermLowerCase) ||
                    listing.Product.ToLower().Contains(searchTermLowerCase) ||
                    (listing.Category.Name.ToLower().Contains(searchTermLowerCase))))
                .Include(e => e.ListingUploads).ThenInclude(e => e.Upload)
                .ToListAsync();
            return results;
        }



    }
}
