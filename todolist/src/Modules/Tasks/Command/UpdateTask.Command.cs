using FluentValidation;
using MediatR;
using todolist.src.Modules.Task.Command;

namespace todolist.src.Modules.Task.Command
{
    public class UpdateTask: IRequest<bool>
    {
        public Guid id { get; set; }
        public string Title { get; set; } = string.Empty;   
        public List<updateTodo> updateTodo { get; set; } = new List<updateTodo>();
    }
}
public  class updateTodo
{
    public string Id { get; set; } =string.Empty;
    public string text { get; set; } = string.Empty;
}
public class UpdateTaskValidator : AbstractValidator<UpdateTask>
{
    public UpdateTaskValidator()
    {
        RuleFor(t => t.Title)
             .NotEmpty()
             .WithMessage("title must not be emty");
    }
}
