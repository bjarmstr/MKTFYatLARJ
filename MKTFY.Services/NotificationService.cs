using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels.Message;
using MKTFY.Models.ViewModels.Notifications;
using MKTFY.Repositories.Repositories.Interfaces;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Services
{
    public class NotificationService: INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public async Task Create (NotificationCreateVM src)
        {
            var newEntity = new Notification(src);
            newEntity.DateSent = DateTime.UtcNow;
            newEntity.Unread = true;
            var result = await _notificationRepository.Create(newEntity);
        }

        public async Task<List<NotificationVM>> Get(string userId)
        {
            var results = await _notificationRepository.Get(userId);
            var models = results.Select(n => new NotificationVM(n)).ToList();
            return models;
        }

        public async Task MarkRead(string userId)
        {
            await _notificationRepository.MarkRead(userId);
            return;

        }


        public async Task<int> UnReadCount(string userId)
        {
            var results = await _notificationRepository.UnReadCount(userId);
            return results;
        }




    }
}
