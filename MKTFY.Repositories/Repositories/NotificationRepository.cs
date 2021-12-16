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
    public class NotificationRepository: INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<Notification> Create(Notification src)
        {
            _context.Add(src);
            await _context.SaveChangesAsync();

            return src;
        }

        public async Task<List<Notification>> Get(string userId)
        {
            var results = await _context.Notifications
               .Where(n => n.UserId == userId)
               .Include(n => n.User)
               .Include(n => n.Message)
               .ToListAsync();

            results.Select(n => n.Unread = false);
            _context.Add(results);
            await _context.SaveChangesAsync();

            return results;
        }

        public async Task MarkRead(string userId)
        {
        
            var results = await _context.Notifications
                .Where(n => n.UserId == userId)
                .ToListAsync();

            //sets all notifications for the user to read
            results.Select(n => n.Unread = false);
            _context.Add(results);
            await _context.SaveChangesAsync();

            return;
        }




    }
}
