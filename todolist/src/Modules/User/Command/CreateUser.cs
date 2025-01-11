using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using To_do_List.src.Modules.User.Command;

namespace To_do_List.src.Modules.User.Command
{
    public class CreateUser : IRequest<bool>
    {
        public required string name { get; set; }
        public required string password { get; set; }
        public required string rePassword { get; set; }
        public required string email { get; set; }

    }
}

public class CreateUserValidator : AbstractValidator<CreateUser>
{
    public CreateUserValidator()
    {
        RuleFor(u => u.name)
            .NotEmpty()
            .WithMessage("password must not be empty");

        RuleFor(u => u.password)
            .NotEmpty()
            .WithMessage("password must not be empty")
            .Matches(@"^(?=.*[A-Z])(?=.*\d).+$")
            .WithMessage("Password must contain at least one uppercase letter and one number.");

        RuleFor(u => u.email)
            .NotEmpty()
            .WithMessage("password must not be empty")
            .EmailAddress()
            .WithMessage("emai must be an email");

        RuleFor(u => u.rePassword)
            .NotEmpty()
            .WithMessage("rePassword must not be emty")
            .Equal(u => u.password)
            .WithMessage("rePassword dose not match password");
    }
}