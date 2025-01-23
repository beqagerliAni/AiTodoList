using MediatR;
using Microsoft.AspNetCore.Mvc;
using todolist.src.Modules.User.Repository;

namespace todolist.src.Modules.User.Command
{
    public class UserDeleteHandler : IRequestHandler<UserDeleteCommand, bool>
    {
        private IUserRepository _userRepository;

        public UserDeleteHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<bool> Handle(UserDeleteCommand request, CancellationToken cancellationToken)
        {
            return _userRepository.Delete();
        }
    }
}
