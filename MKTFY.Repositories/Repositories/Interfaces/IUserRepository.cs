using MKTFY.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Repositories.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Create(User src);
        Task<User> Get(string id);
        Task<User> Update(User src);
    }
}
