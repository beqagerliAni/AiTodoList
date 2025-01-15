using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace todolist.Helper.Jwt
{
    public  class JwtDecode
    { 
        public static IHttpContextAccessor? _contextAccessor {  get; set; }
        public static Task<JwtSecurityToken> DecodeAuthToken()
        {
            HttpContextAccessor httpContext = new HttpContextAccessor();
            if (httpContext.HttpContext == null) { throw new NotImplementedException(); }

            string? token =  httpContext.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token), "Token cannot be null or empty.");

            try
            {

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

              
                if (!handler.CanReadToken(token))
                    throw new ArgumentException("Invalid JWT token format.");

                JwtSecurityToken jwtToken = handler.ReadJwtToken(token);
                


                return   Task.FromResult(jwtToken);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to decode the JWT token.", ex);
            }
        }
        public static Task<string>  Decode(string token)
        {
            if (string.IsNullOrEmpty(token))
                throw new ArgumentNullException(nameof(token), "Token cannot be null or empty.");

            try
            {

                var handler = new JwtSecurityTokenHandler();


                if (!handler.CanReadToken(token))
                    throw new ArgumentException("Invalid JWT token format.");


                var jwtToken = handler.ReadJwtToken(token);

                var payload = jwtToken.Payload;
                

                return Task.FromResult(JsonSerializer.Serialize(payload));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to decode the JWT token.", ex);
            }

        }
    }
}
