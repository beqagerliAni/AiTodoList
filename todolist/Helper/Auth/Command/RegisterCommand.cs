using FluentValidation;
using MediatR;
using To_do_List.src.Modules.User.Command;
using todolist.Helper.Auth.Command;

namespace todolist.Helper.Auth.Command
{
    public class RegisterCommand: CreateUser
    {
    }
}
public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        Include(new CreateUserValidator());
    }

}
