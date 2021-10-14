using MKTFY.Models.ViewModels.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
   public interface IAuthService
    {
        Task<AuthResponseVM> ExchangeToken(string accessCode);
    }
}
