using MKTFY.Models.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserVM> Create(UserCreateVM data, string userId);

        Task<UserVM> Get(string id, string userId);

        Task<UserVM> Update(UserUpdateVM data, string userId);

        Task<UserVM> UpdateStatus(string userId, string status);
    }
}
