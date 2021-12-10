using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Models.ViewModels.Message
{
    public class MessageVM
    {
        public MessageVM()
        {
        }

        public MessageVM(Entities.Message src)
        {
            Message = src.Content;
            Id = src.Id;
        }

        public Guid Id { get; set; }
        public string Message { get; set; }

    }
}
