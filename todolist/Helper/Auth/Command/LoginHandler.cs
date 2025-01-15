using MediatR;
using todolist.Helper.Auth.Service;

namespace todolist.Helper.Auth.Command
{
    public class LoginHandler : IRequestHandler<LoginCommand, string>
    {
        IAuthService _authService;
        public LoginHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authService.Login(request);
        }
    }
}
