using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace todolist.src.Modules.User.Command
{
    public class UserDeleteCommand : IRequest<bool>
    {
        public required Guid Id { get; set; }
    }
}
