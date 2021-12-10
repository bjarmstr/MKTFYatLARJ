using MKTFY.Models.ViewModels.Message;
using MKTFY.Models.ViewModels.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
    public interface INotificationService
    {
        Task Create(NotificationCreateVM src);

        Task<List<NotificationVM>> Get(string userId);

    }
}
