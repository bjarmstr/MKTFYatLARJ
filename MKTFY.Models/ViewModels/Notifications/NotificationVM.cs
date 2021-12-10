using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.ViewModels.Notifications
{
    public class NotificationVM
    {

        public NotificationVM()
        {

        }

        public NotificationVM(Notification src)
        {
            Message = src.Message.Content;
            DateSent = src.DateSent;
            Unread = src.Unread;
        }

        public string Message { get; set; }

        public DateTime DateSent { get; set; }
        
        public bool Unread { get; set; }



    }
}
