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

    
        public async Task<string> CreateOrUpdate(string accessToken)
        //return a string so it can hold error message or null for success
        //another alternative is to create a custom exception handler and catch the exception in the controller
        {
            var result = await ProfileRequest(accessToken);
            if (result == null)
                return "Unable to update the user profile";

            // Get the existing user
            var user = await _userRepository.GetById(result.sub);

            //map Auth0 metadata fields <UserInfoResponseMeta> to <User>
            var userData = new User
            {
                Id = result.sub,
                Email = result.email,
                FirstName = result.user_metadata?.firstName,
                LastName = result.user_metadata?.lastName,
                Phone = result.user_metadata?.phone,
               StreetAddress = result.user_metadata?.address,
               City = result.user_metadata?.city,
               //province not included in metadata
              //Province = result.user_metadata?.province,
              Province = null,
              Country = result.user_metadata?.country
      
        };


            if (user == null)
            {
                userData.DateCreated = DateTime.UtcNow;
                await _userRepository.Create(userData);
            }
            else
                await _userRepository.Update(userData);

            return null;
        }
        // Get the user's profile information from Auth0
        private async Task<UserInfoResponse> ProfileRequest(string accessToken)
        {
            var authUrl = _configuration.GetSection("Auth0").GetValue<string>("Domain");

            // Build the request
            var req = new HttpRequestMessage(HttpMethod.Get, authUrl + "/userInfo");
            req.Headers.Add("Authorization", "Bearer " + accessToken);
            var res = await _httpClient.SendAsync(req);

            if (!res.IsSuccessStatusCode)
                return null;

            var result = await res.Content.ReadFromJsonAsync<UserInfoResponse>();
            return result;
        }
        public class UserInfoResponse
        {
            public string sub { get; set; }

            public string email { get; set; }

            [JsonPropertyName("http://schemas.mktfy.com/user_metadata")]
            public UserInfoResponseMeta user_metadata { get; set; }
        }

        public class UserInfoResponseMeta
        {
            public string firstName { get; set; }

            public string lastName { get; set; }

            public string phone { get; set; }

            public string country { get; set; }

            public string city { get; set; }

            public string address { get; set; }
        }
    }
}
