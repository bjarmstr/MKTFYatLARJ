using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Repositories.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<Notification> Create(Notification src);
        Task<List<Notification>> Get(string userId);
    }
}
