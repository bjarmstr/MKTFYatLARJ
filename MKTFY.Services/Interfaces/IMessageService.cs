using MKTFY.Models.ViewModels.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
    public interface IMessageService
    {
        Task<MessageVM> CreateMessage(string data);
    }
}
