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
    public class FAQRepository : IFAQRepository
    {
        private readonly ApplicationDbContext _context;

        public FAQRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FAQ> Create(FAQ src)
        {
            _context.FAQs.Add(src);
            await _context.SaveChangesAsync();
            return src;
        }

        public async Task<FAQ> Get(Guid id)
        {
            var result = await _context.FAQs.FirstOrDefaultAsync(i => i.Id == id);

            if (result == null) throw new NotFoundException("The requested listing could not be found");
            return result;
        }
        public async Task<List<FAQ>> GetAll(bool isDeleted)
        {
            var result = await _context.FAQs
                .Where(faq=> faq.IsDeleted == isDeleted)
                .ToListAsync();
            return result;
        }


    public async Task<FAQ> Update(FAQ src)
    {
            var result = await _context.FAQs.FirstOrDefaultAsync(i => i.Id == src.Id);
            if (result == null) throw new NotFoundException("The requested listing could not be found");
            result.Id = src.Id;
            result.Question = src.Question;
            result.Answer = src.Answer;
            result.IsDeleted = src.IsDeleted;
            await _context.SaveChangesAsync();
            return result;
        }


        public async Task SoftDelete(Guid id, Boolean isDeleted)
        {
            var result = await _context.FAQs.FirstOrDefaultAsync(i => i.Id == id);
            if (result == null) throw new NotFoundException("The requested listing could not be found");
            result.IsDeleted = isDeleted;
            await _context.SaveChangesAsync();
         }


    }
}
