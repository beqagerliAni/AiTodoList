using todolist.Helper.Auth.Command;

namespace todolist.Helper.Auth.Service
{
    public interface IAuthService
    {
        public Task<bool> Register(RegisterCommand command);
        public Task<string> Login(LoginCommand command);
    }
}
