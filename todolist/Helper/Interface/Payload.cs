using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace todolist.Helper.Interface
{
    public class Payload
    {
        public required ClaimsIdentity Identity { get; set; }
        public required AuthenticationProperties Properties { get; set; }
    }
}
