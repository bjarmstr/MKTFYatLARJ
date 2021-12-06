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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> Create(User src)
        {
            _context.Users.Add(src);
            await _context.SaveChangesAsync();
            return src;
        }

        public async Task<User> Get(string id)
        {
            var result = await _context.Users
            .FirstOrDefaultAsync(i => i.Id == id);
            return result;
        }

        public async Task<User> Update(User src)
        {
            var result = await _context.Users.FirstOrDefaultAsync(i => i.Id == src.Id);
            if (result == null) throw new NotFoundException("The requested user could not be found");
            //update some of the user fields but not all --DateCreated, Email and Status
            result.Id = src.Id;
            result.FirstName = src.FirstName;
            result.LastName = src.LastName;
            result.Phone = src.Phone;
            result.StreetAddress = src.StreetAddress;
            result.City = src.City;
            result.Province = src.Province;
            result.Country = src.Country;

            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<User> UpdateStatus(string userId, string status)
        {
            var result = await _context.Users.FirstOrDefaultAsync(i => i.Id == userId);
            if (result == null) throw new NotFoundException("The requested user could not be found");
            result.Status = status;

            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<List<User>> GetAllActiveUsers(int pageIndex, int pageSize)
        {
            var result = await _context.Users
                .Where(user => user.Status != "blocked")
                .OrderBy(user => user.DateCreated)
                .Skip((pageIndex - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return result;

        }


    }
}
