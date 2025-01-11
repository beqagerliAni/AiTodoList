using MediatR;
using To_do_List.src.Modules.User.Model;

namespace todolist.src.Modules.User.Command
{
    public class FindOneUserCommand: IRequest<UserModel>
    {
        public required Guid Id { get; set; }
    }
}
