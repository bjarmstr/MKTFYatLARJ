using Microsoft.EntityFrameworkCore;
using MKTFY.Models.Entities;
using MKTFY.Repositories.Repositories.Interfaces;
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
            var result = await _context.Listings.FirstAsync(i => i.Id == id);
            return result;
        }

        public async Task<List<Listing>> GetAll()
        {
            var results = await _context.Listings.ToListAsync();
            return results;
        }

        public async Task<Listing> Update(Listing src)
        {
            var result = await _context.Listings.FirstAsync(i => i.Id == src.Id);
            result.Id = src.Id;
            result.Product = src.Product;
            result.Details = src.Details;
            result.Price = src.Price;
            result.CategoryId = src.CategoryId;
           // result.DateCreated = src.DateCreated;
           //sellerID 
            //result.TransactionStatus = src.TransactionStatus;


            await _context.SaveChangesAsync();
            return result;
        }

        public async Task Delete(Guid id)
        {
            var result = await _context.Listings.FirstAsync(i => i.Id == id);
            _context.Remove(result);
            await _context.SaveChangesAsync();

        }
    }
}
