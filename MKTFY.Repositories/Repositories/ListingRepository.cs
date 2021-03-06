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
    public class ListingRepository: IListingRepository
    {
        private readonly ApplicationDbContext _context;

        public ListingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Listing> Create(Listing src)
        {
            _context.Listings.Add(src);
            await _context.SaveChangesAsync();
            return src;
        }

       
        public async Task<Listing> Get(Guid id)
        {
            var result = await _context.Listings
                .FirstOrDefaultAsync(i => i.Id == id);

            if (result == null) throw new NotFoundException("The requested listing could not be found");
            return result;
        }

        public async Task<List<Listing>> GetAll()
        {
            var results = await _context.Listings
                .ToListAsync();
            return results;
        }

        public async Task<Listing> Update(Listing src)
        {
            var result = await _context.Listings.FirstOrDefaultAsync(i => i.Id == src.Id);
            if (result == null) throw new NotFoundException("The requested listing could not be found");
            result.Id = src.Id;
            result.Product = src.Product;
            result.Details = src.Details;
            result.Price = src.Price;
            result.CategoryId = src.CategoryId;


            await _context.SaveChangesAsync();
            return result;
        }

        public async Task Delete(Guid id)
        {
            var result = await _context.Listings.FirstOrDefaultAsync(i => i.Id == id);

            if (result == null) throw new NotFoundException("The requested listing could not be found");

            _context.Remove(result);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Listing>> GetByCategory(int categoryId)
        {
            //**need to take region into account@@@jma
            var results = await _context.Listings
                .Where(listing => listing.CategoryId == categoryId)
                .ToListAsync();
            return results;
        }

        public async Task<List<Listing>> GetBySearchTerm(string searchTerm)
        {
            //**need to take region into account@@@jma
            var results = await _context.Listings
                .Where(listing => listing.Details.ToLower().Contains(searchTerm) || listing.Product.ToLower().Contains(searchTerm))
                .ToListAsync();
            return results;
        }



    }
}
