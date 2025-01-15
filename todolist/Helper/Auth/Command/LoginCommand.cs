using MediatR;

namespace todolist.Helper.Auth.Command
{
    public class LoginCommand: IRequest<string>
    {
        public required string email {  get; set; }
        public required string password { get; set; }
    }
}
