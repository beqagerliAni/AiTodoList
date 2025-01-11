using FluentValidation;
using MediatR;
using To_do_List.src.Modules.Task.Command;
using To_do_List.src.Modules.Task.Module;

namespace To_do_List.src.Modules.Task.Command
{
    public class CreateTask: IRequest<bool>
    {
        public required string Title { get; set; }
        public List<string>? text { get; set; } = [];
    }
}
public class CreateTaskValidator  : AbstractValidator<CreateTask>
{
    public CreateTaskValidator()
    {
        RuleFor(t => t.Title)
             .NotEmpty()
             .WithMessage("title must not be emty");

    }
}
