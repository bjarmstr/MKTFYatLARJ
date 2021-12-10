using MKTFY.Models.ViewModels.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.Entities
{
    public class Notification
    {
        public Notification()
        {

        }

        public Notification(NotificationCreateVM src )
        {
            UserId = src.UserId;
            MessageId = src.MessageId;

        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        [Required]
        public Guid MessageId { get; set; }

        /// <summary>
        /// Message is a navigation property, which allows access to Message details
        /// </summary>
        public Message Message { get; set; }

        [Required]
        public DateTime DateSent { get; set; }

        public bool Unread { get; set; }


    }
}
