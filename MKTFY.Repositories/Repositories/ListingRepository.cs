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

        public async Task<Listing> GetListingWithSeller(Guid id)
        {
            var result = await _context.Listings
                .Include(e => e.ListingUploads).ThenInclude(e => e.Upload)
                .Include(e=>e.User)
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
                    //remove from listing and listingUpload 
                    result.ListingUploads.Remove(upload);
                    //remove image from Uploads
                    _context.Uploads.Remove(upload.Upload);
                }
            }
          
            //add any new images to listingUploads
            foreach(var uploadId in srcNotResult)
            {
                var listingUpload = src.ListingUploads.First(i => i.UploadId == uploadId);
                //refactor to eliminate second call back to repo in service layer TODO @@@jma 
                //add to the listing 
                result.ListingUploads.Add(listingUpload);
            }

       
            result.Product = src.Product;
            result.Details = src.Details;
            result.Price = src.Price;
            result.CategoryId = src.CategoryId;
            result.Condition = src.Condition;
            result.Address = src.Address;
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
           
            //remove from listing and listingUpload
            _context.Remove(result);

            //remove from Upload
            foreach (var listingUpload in result.ListingUploads)
            {
                var upload = listingUpload.Upload;
                _context.Remove(upload);
            }

            await _context.SaveChangesAsync();
            
        }

        public async Task<List<Listing>> GetByCategory(int categoryId, string region, string userId)
        {
            var results = await _context.Listings
                .Where(listing => listing.CategoryId == categoryId &&
                    listing.Region == region &&
                    listing.TransactionStatus== "listed" &&
                    listing.UserId != userId)
                .Include(e => e.ListingUploads).ThenInclude(e => e.Upload)
                .ToListAsync();
            return results;
        }

        public async Task<List<Listing>> GetBySearchTerm(string searchTermLowerCase, string region, string userId)
        {
            var results = await _context.Listings
                .Where(listing => listing.Region == region 
                    && listing.TransactionStatus == "listed"
                    && listing.UserId != userId &&
                   (listing.Details.ToLower().Contains(searchTermLowerCase) ||
                    listing.Product.ToLower().Contains(searchTermLowerCase) ||
                    (listing.Category.Name.ToLower().Contains(searchTermLowerCase))))
                .Include(e => e.ListingUploads).ThenInclude(e => e.Upload)
                .ToListAsync();
            return results;
        }


        public async Task<List<Listing>> GetMostRecent(string region, string userId, int listingCount)
        {
            var results = await _context.Listings
                .Where(listing => listing.UserId != userId  &&
                listing.Region == region &&
                listing.TransactionStatus == "listed")
                .OrderByDescending(listing => listing.DateCreated)
                .Take(listingCount)
                .Include(e => e.ListingUploads).ThenInclude(e => e.Upload)
                .ToListAsync();
            return results;
        }

        public async Task<Listing> GetPickupInfo(Guid id)
        {
            var result = await _context.Listings
                .Include(listing => listing.User)
                .Include(e => e.ListingUploads).ThenInclude(e => e.Upload)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (result == null) throw new NotFoundException("The requested listing could not be found");            
            return result;

        }

        public async Task<List<Listing>> GetMyPurchases(string buyerId)
        {
            var results = await _context.Listings
               .Where(listing => listing.BuyerId == buyerId)
               .Include(e => e.ListingUploads).ThenInclude(e => e.Upload)
               .ToListAsync();
            return results;
        }

        public async Task<List<Listing>> GetMyListings(string userId, string status)
        {
            var results = await _context.Listings
               .Where(listing => listing.UserId == userId && listing.TransactionStatus == status)
               .Include(e => e.ListingUploads).ThenInclude(e => e.Upload)
               .ToListAsync();
            return results;
        }


        public async Task<int> GetMyListingsCount(string userId, string status)
        {
            var results = await _context.Listings
               .Where(listing => listing.UserId == userId && listing.TransactionStatus == status)
               .CountAsync();
            return results;
        }

        public async Task<int> GetMyPurchasesCount(string buyerId)
        {
            var results = await _context.Listings
               .Where(listing => listing.BuyerId == buyerId)
               .CountAsync();
            return results;
        }

        public async Task<List<Listing>> GetAllMyListings(string userId)
        { 
            //does not return listings that are in deleted status
            var results = await _context.Listings
               .Where(listing => listing.UserId == userId && listing.TransactionStatus != "deleted")
               .Include(e => e.ListingUploads).ThenInclude(e => e.Upload)
               .ToListAsync();
            return results;
        }


   
        public async Task ChangeTransactionStatus(Guid id, string status, string buyerId)
        {
            var result = await _context.Listings
                .Include(e => e.User)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (result == null) throw new NotFoundException("The requested listing could not be found");

            if (status=="listed"| status=="pending")
                {
                result.BuyerId = buyerId;
            }

            if (status == "sold")
            {
                result.DateSold = DateTime.UtcNow;
            }

            //TODO @@@jma check for incorrect status changes --should not be able to change sold items
            //how do you handle this error correctly?
            if (result.TransactionStatus=="sold") throw new Exception("Unable to perform changes");
            result.TransactionStatus = status;
           
            await _context.SaveChangesAsync();
        }


   
    }
}
