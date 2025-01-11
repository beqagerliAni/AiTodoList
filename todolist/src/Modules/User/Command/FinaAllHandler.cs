using MediatR;
using To_do_List.src.Modules.User.Model;
using todolist.src.Modules.User.Repository;

namespace todolist.src.Modules.User.Command
{
    public class FinaAllHandler : IRequestHandler<FindAllCommand, List<UserModel>>
    {
        IUserRepository _userRepository;
        public FinaAllHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<List<UserModel>> Handle(FindAllCommand request, CancellationToken cancellationToken)
        {
            return _userRepository.FindAll();
        }
    }
}
