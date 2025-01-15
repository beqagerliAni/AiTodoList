using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace todolist.Helper.Jwt
{
    public static class JwtGenerate
    {

        public static  async Task<string> GenerateJwtAsync(int expires,Guid id)
        {

            ConfigurationBuilder builder = new ConfigurationBuilder();

            string key = await Configuration.Configuration.Connection("SecurityKey");
            Console.WriteLine(key);
             
            byte[] encodedString = Encoding.UTF8.GetBytes(key);

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(encodedString);

            SigningCredentials credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Role, "User"),
                    new Claim(ClaimTypes.NameIdentifier, id.ToString())
                };

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: await Configuration.Configuration.Connection("issuer"),
                audience: await  Configuration.Configuration.Connection("audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddHours(expires),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
