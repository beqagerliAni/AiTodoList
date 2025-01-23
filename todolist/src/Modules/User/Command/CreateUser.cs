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
            .WithMessage("New password is required.")
            .MinimumLength(8)
            .WithMessage("New password must be at least 8 characters long.")
            .Matches("[A-Z]")
            .WithMessage("New password must contain at least one uppercase letter.")
            .Matches("[a-z]")
            .WithMessage("New password must contain at least one lowercase letter.")
            .Matches("[0-9]")
            .WithMessage("New password must contain at least one digit.")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("New password must contain at least one special character.");

        RuleFor(u => u.email)
           .NotEmpty()
            .WithMessage("email must not be empty")
            .EmailAddress()
            .WithMessage("email must be valid");


        RuleFor(u => u.rePassword)
            .NotEmpty()
            .WithMessage("rePassword must not be emty")
            .Equal(u => u.password)
            .WithMessage("rePassword dose not match password");
    }
}