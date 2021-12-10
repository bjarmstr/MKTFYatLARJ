﻿using MKTFY.Models.ViewModels.Message;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.Entities
{
    public class Message
    {
        public Message()
        {

        }

        public Message(MessageVM src)
        {
            Content = src.Message;

        }

        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

    }
}
