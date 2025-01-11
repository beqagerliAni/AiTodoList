using MediatR;
using Microsoft.AspNetCore.Mvc;
using To_do_List.src.Modules.User.Command;
using todolist.src.Modules.User.Repository;

namespace todolist.src.Modules.User.Command
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, bool>
    {
        IUserRepository _userRepository;
        public UpdateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        Task<bool> IRequestHandler<UpdateUserCommand, bool>.Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            return _userRepository.Update(request);
        }
    }
}
