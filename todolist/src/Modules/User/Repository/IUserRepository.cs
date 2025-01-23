using Microsoft.AspNetCore.Mvc;
using To_do_List.src.Modules.User.Command;
using To_do_List.src.Modules.User.Model;
using todolist.src.Modules.User.Command;

namespace todolist.src.Modules.User.Repository
{
    public interface IUserRepository
    {
        public Task<bool> Create(CreateUser command);
        public Task<bool> Delete();
        public Task<bool> Update(UpdateUserCommand command);
        public Task<UserModel> FindOne();
        public Task<List<UserModel>> FindAll();
        public UserModel findByEmail(string email);
    }
}
