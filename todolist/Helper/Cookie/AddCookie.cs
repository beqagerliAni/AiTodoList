using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using todolist.Helper.Interface;

namespace todolist.Helper.Cookie
{
    public  class AddCookie
    {
        public static  Task<Payload> AddCookieAuth(Guid id)
        {
            var claims = new List<Claim>
            {

                new Claim(ClaimTypes.Name, id.ToString()),
                new Claim(ClaimTypes.Role, "User"),

            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                IsPersistent = true,
                IssuedUtc = DateTime.UtcNow,
                RedirectUri = "https://facebook.com"
            };
            return Task.FromResult(new Payload { Identity = claimsIdentity, Properties = authProperties });
        }
    }
}
