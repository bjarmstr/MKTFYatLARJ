using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
    public interface IUserService
    {
        Task<string> CreateOrUpdate(string accessToken);
    }
}
