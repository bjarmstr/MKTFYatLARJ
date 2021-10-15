using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly HttpClient _httpClient;

        public UserService(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _httpClient = new HttpClient();
        }

        public async Task<UserVM> Create(UserCreateVM src, string accessToken )
        {
            var newEntity = new User(src);
            
            newEntity.DateCreated = DateTime.UtcNow;

            var authData = await ProfileRequest(accessToken);
            //if (result == null)
            //**Create a custom exception handler@@@@@@@@@
            //return "Unable to update the user profile";
            newEntity.Id = authData.sub;
            newEntity.Email = authData.email;
            //check if authData.sub already exists in database@@@@
            //error that user already exists if found
            var result = await _userRepository.Create(newEntity);
            var model = new UserVM(result);
            return model;

        }

  
        // Get the user's profile information from Auth0
        private async Task<UserInfoResponse> ProfileRequest(string accessToken)
        {
            
            var authUrl = _configuration.GetSection("Auth0").GetValue<string>("Domain");

            // Build the request
            var req = new HttpRequestMessage(HttpMethod.Get, authUrl + "/userInfo");
            req.Headers.Add("Authorization", "Bearer " + accessToken);
            var res = await _httpClient.SendAsync(req);

            var result = await res.Content.ReadFromJsonAsync<UserInfoResponse>();
            return result;
        }
        public class UserInfoResponse
        {
            public string sub { get; set; }

            public string email { get; set; }

   
        }

    
    }
}
