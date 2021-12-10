using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels.Message;
using MKTFY.Repositories.Repositories.Interfaces;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        public MessageService(IMessageRepository messageRepository)
        {
            _messageRepository = messageRepository;

        }

        public async Task<MessageVM> CreateMessage(string data)
        {

            var newEntity = new Message()
            {
                Content = data,
                DateCreated = DateTime.UtcNow
            };
            var results = await _messageRepository.CreateMessage(newEntity);

            var model = new MessageVM(results);

            return model;
        }

    }
}
