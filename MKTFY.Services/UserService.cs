using Microsoft.Extensions.Configuration;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels.User;
using MKTFY.Repositories.Repositories.Interfaces;
using MKTFY.Services.Interfaces;
using MKTFY.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MKTFY.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;

        }

        public async Task<UserVM> Create(UserCreateVM src, string userId)
        {
            var newEntity = new User(src);
            //check email matches Auth0 @@@jma
            //TODO type of Exception
            if (newEntity.Id != userId) throw new Exception("userId mismatch");
            newEntity.Status = "active";
            newEntity.DateCreated = DateTime.UtcNow;
            var result = await _userRepository.Create(newEntity);
            var model = new UserVM(result);
            return model;

        }

        public async Task<UserVM>Get(string id, string userId)
        {
            ///TODO Exception --- Authorization Exception?
            if (id != userId) throw new Exception("userId mismatch");
            var result = await _userRepository.Get(id);
          
            var model = new UserVM(result);
            return model;
        }

        public async Task <UserVM>Update(UserUpdateVM src, string userId)
        {
            if (src.Id != userId) throw new Exception("userId mismatch");
            var updateData = new User(src);
            var result = await _userRepository.Update(updateData);
            var model = new UserVM(result);
            return model;

        }

        public async Task<UserVM> UpdateStatus(string userId,  string status)
        {
            var result = await _userRepository.UpdateStatus(userId,status);
            var model = new UserVM(result);
            return model;

        }


    }
}
