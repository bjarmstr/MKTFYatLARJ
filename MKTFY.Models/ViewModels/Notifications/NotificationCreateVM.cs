using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.ViewModels.Notifications
{
    public class NotificationCreateVM
    {

        public NotificationCreateVM()
        {

        }

        public Guid MessageId { get; set; }
        
        public string UserId { get; set; }

    }
}
