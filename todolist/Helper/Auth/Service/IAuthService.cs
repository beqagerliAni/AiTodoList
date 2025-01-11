using todolist.Helper.Auth.Command;
using todolist.Helper.Interface;

namespace todolist.Helper.Auth.Service
{
    public interface IAuthService
    {
        public Task<bool> Register(RegisterCommand command);
        public Task<Payload> Login(LoginCommand command);
    }
}
