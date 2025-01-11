using MediatR;
using Microsoft.AspNetCore.Mvc;
using todolist.src.Modules.User.Repository;

namespace To_do_List.src.Modules.User.Command
{
    public class CreateUserHandler : IRequestHandler<CreateUser, bool>
    {
        IUserRepository _userRepository;
        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<bool> Handle(CreateUser request, CancellationToken cancellationToken)
        {
           return _userRepository.Create(request);
        }
    }
}
