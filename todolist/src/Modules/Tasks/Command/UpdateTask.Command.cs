using MediatR;
using To_do_List.src.Modules.Task.Command;

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
