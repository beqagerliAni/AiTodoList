using MediatR;
using todolist.Helper.Auth.Service;

namespace todolist.Helper.Auth.Command
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, bool>
    {
        IAuthService _authService;
        public RegisterHandler(IAuthService authService)
        {
            _authService = authService;
        }
        public async Task<bool> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            return await _authService.Register(request);
        }
    }
}
