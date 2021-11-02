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
            var result = await _context.Listings
                 .Include(e => e.ListingUploads).ThenInclude(e => e.Upload)
                 .FirstOrDefaultAsync(i => i.Id == src.Id);
            if (result == null) throw new NotFoundException("The requested listing could not be found");

            var listingUploadsSrc = await _context.ListingUploads
                .Include(e=>e.Upload)
                .Where(e => e.ListingId == src.Id)
                .ToListAsync();

            var listofResultUploadIds = result.ListingUploads.Select(e => e.UploadId).ToList();
            var listofSrcUploadIds = listingUploadsSrc.Select(e => e.UploadId).ToList();
            var resultNotSrc = listofResultUploadIds.Except(listofSrcUploadIds).ToList();
            //var firstNotSecond = list1.Except(list2).ToList();
            //delete files required for deletion and link to listing
            //exception for null image TODO@@@jma 

            //compare 2 lists & keep values that occur in result & not source

            //delete upload & ListingUpload reference
            //JASON if I call context again will it replace the other context or add to it?
            var resultUpload = await _context.Uploads
                                .FirstOrDefaultAsync(i => i.Id == uploadId);
                        _context.Remove(resultUpload);
                        await _context.SaveChangesAsync();
                        ///TODO jma -does it delete the link table too?
        


            result.Id = src.Id;
            result.Product = src.Product;
            result.Details = src.Details;
            result.Price = src.Price;
            result.CategoryId = src.CategoryId;
            result.Condition = src.Condition;
            result.Region = src.Region;
            result.ListingUploads = src.ListingUploads;
           
            //doesn't update DateCreated or TransactionStatus
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<ICollection<ListingUpload>> Delete(Guid id)
        {
            //delete images associated with Listing TODO@@@jma
            var result = await _context.Listings
                .Include(e =>e.ListingUploads).ThenInclude(e=>e.Upload)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (result == null) throw new NotFoundException("The requested listing could not be found");
            
            //delete uploads
           // var uploadResults = await _context.Uploads
            //    .Where(upload => upload.Id == ListingUploads.UploadId)

            //delete
            _context.Remove(result);
            await _context.SaveChangesAsync();
            return result.ListingUploads;
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
