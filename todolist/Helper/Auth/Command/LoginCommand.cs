using MediatR;
using todolist.Helper.Interface;

namespace todolist.Helper.Auth.Command
{
    public class LoginCommand: IRequest<Payload>
    {
        public required string email {  get; set; }
        public required string password { get; set; }
    }
}
