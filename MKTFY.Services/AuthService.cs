using Microsoft.Extensions.Configuration;
using MKTFY.Models.ViewModels.Auth;
using MKTFY.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.Services
{

    public class AuthService : IAuthService
    {
        private IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
        }

        public async Task<AuthResponseVM> ExchangeToken(string accessCode)
        {
            // Perform the exchange of the Auth Code for an Access Token
            var result = await TokenRequest(accessCode);

            if (result == null)
                return null;

            return new AuthResponseVM
            {
                AccessToken = result.access_token,
                ExpiresIn = result.expires_in
            };

        }

        // Helpers
        private async Task<AuthResponse> TokenRequest(string authCode)
        {
            var clientId = _configuration.GetSection("Auth0").GetValue<string>("ClientId");
            var clientSecret = _configuration.GetSection("Auth0").GetValue<string>("ClientSecret");
            var authUrl = _configuration.GetSection("Auth0").GetValue<string>("Domain");
            var audience = _configuration.GetSection("Auth0").GetValue<string>("Audience");
            var redirectUrl = _configuration.GetSection("Auth0").GetValue<string>("TokenRedirectUrl");

            // Configure the request
            var dict = new Dictionary<string, string>();
            dict.Add("Content-Type", "application/x-www-form-url-encoded");
            dict.Add("grant_type", "authorization_code");
            dict.Add("code", authCode);
            dict.Add("client_id", clientId);
            dict.Add("client_secret", clientSecret);
            dict.Add("redirect_uri", redirectUrl);
            dict.Add("audience", audience);

            // Build the request
            var req = new HttpRequestMessage(HttpMethod.Post, authUrl + "/oauth/token")
            {
                Content = new FormUrlEncodedContent(dict)
            };
            var res = await _httpClient.SendAsync(req);

            if (!res.IsSuccessStatusCode)
                return null;

            var result = await res.Content.ReadFromJsonAsync<AuthResponse>();

            return result;
        }
        public class AuthResponse
        {
            public string id_token { get; set; }
            public string access_token { get; set; }
            public string refresh_token { get; set; }
            public int expires_in { get; set; }
            public string token_type { get; set; }
        }
    }
}
