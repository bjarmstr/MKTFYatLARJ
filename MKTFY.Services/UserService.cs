﻿using Microsoft.Extensions.Configuration;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels.User;
using MKTFY.Repositories.Repositories.Interfaces;
using MKTFY.Services.Interfaces;
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

        public async Task<UserVM> Create(UserCreateVM src)
        {
            var newEntity = new User(src);

            newEntity.DateCreated = DateTime.UtcNow;
            var result = await _userRepository.Create(newEntity);
            var model = new UserVM(result);
            return model;

        }

        public async Task<UserVM>Get(string id)
        {
            var result = await _userRepository.Get(id);
            var model = new UserVM(result);
            return model;
        }
  
    
    }
}
