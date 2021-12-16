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

        /// <summary>
        /// personalized message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// recipient of message
        /// </summary>
        public string UserId { get; set; }

    }
}
