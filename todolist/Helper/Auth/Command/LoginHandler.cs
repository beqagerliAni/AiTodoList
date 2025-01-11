using MediatR;
using todolist.Helper.Auth.Service;
using todolist.Helper.Interface;

namespace todolist.Helper.Auth.Command
{
    public class LoginHandler : IRequestHandler<LoginCommand, Payload>
    {
        IAuthService _authService;
        public LoginHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<Payload> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            return await _authService.Login(request);
        }
    }
}
