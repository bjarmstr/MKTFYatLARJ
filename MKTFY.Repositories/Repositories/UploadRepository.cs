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
    public class UploadRepository: IUploadRepository
    {
        private readonly ApplicationDbContext _context;

        public UploadRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Upload> Create(Upload src)
        {
            _context.Uploads.Add(src);         
            await _context.SaveChangesAsync(); 
            return src;
        }

        // Get a single file upload by Id
        public async Task<Upload> Get(Guid id)
        {
            var result = await _context.Uploads.FirstOrDefaultAsync(i => i.Id == id);
            if (result == null)
                throw new NotFoundException("The requested upload could not be found");
            return result;
        }

        public async Task Delete(Guid id)
        {
            var result = await _context.Uploads.FirstOrDefaultAsync(i => i.Id == id);
            if (result == null)
                throw new NotFoundException("The requested upload could not be found");
            // Remove the entity from the collection in memory
            _context.Remove(result);
            // Remove the entity from the real database
            await _context.SaveChangesAsync();
        }

    }
}
