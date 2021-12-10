using MKTFY.Models.Entities;
using MKTFY.Repositories.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Repositories.Repositories
{
    public class MessageRepository: IMessageRepository
    {
        private readonly ApplicationDbContext _context;
        public MessageRepository()
        {

        }

        public async Task<Message> CreateMessage(Message src)
        {
            _context.Add(src);
            await _context.SaveChangesAsync();
            return src;
        }
    }
}
