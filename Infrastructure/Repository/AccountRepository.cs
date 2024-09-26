using Application.Interface;
using Domain.Entities.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using RestSharp;
using RestSharp.Authenticators;
using System.Net;

namespace Infrastructure.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _config;
        public AccountRepository(UserManager<ApplicationUser> userManager,
                                     SignInManager<ApplicationUser> signInManager,
                                     IHttpContextAccessor httpContextAccessor,
                                     IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _config = config;
        }

        public async Task<bool> Register(Register user)
        {
            var userToAdd = new ApplicationUser()
            {
                FirstName = user.FirstName,
                UserName = user.Email,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            var result = await _userManager.CreateAsync(userToAdd, user.Password);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(userToAdd);
                var callbackUrl = $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host}/api/Account/ConfirmEmail?userId={userToAdd.Id}&code={WebUtility.UrlEncode(token)}";

                var body = $"Please confirm your email address <a href=\"{callbackUrl}\">Click Here</a>";

                var res = SendEmail(body, userToAdd.Email);
                return true;
            }
            return false;
        }

        public Task<bool> Login(Login login)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ConfirmEmail(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return true;
            }

            return false;
        }

        private bool SendEmail(string body,string email)
        {
            var client = new RestClient("https://api.mailgun.net/v3");
            

            var request = new RestRequest("", Method.Post);

            client.Authenticator = new HttpBasicAuthenticator("api", _config.GetSection("MailGun:ApiKey").Value);

            request.AddParameter("domain", "sandbox04cc8b2c1c8242188f79f33118dffb13.mailgun.org", ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from", "Excited User <postmaster@sandbox04cc8b2c1c8242188f79f33118dffb13.mailgun.org>");
            request.AddParameter("to", email);
            request.AddParameter("subject", "Hello");
            request.AddParameter("html", body, ParameterType.RequestBody);
            request.Method = Method.Post;

            var response = client.Execute(request);

            return response.IsSuccessful;
        }
    }
}
