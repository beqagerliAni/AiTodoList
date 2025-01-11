using System.Data.Entity;
using To_do_List.src.Modules.User.Command;
using To_do_List.src.Modules.User.Entity;
using To_do_List.src.Modules.User.Model;
using todolist.src.Modules.User.Command;

namespace todolist.src.Modules.User.Repository
{
    public class UserRepository : IUserRepository
    {
        appDbcontext _dbcontext;
        public UserRepository(appDbcontext appDbcontext)
        {
            _dbcontext = appDbcontext;
            
        }
        public async Task<bool> Create(CreateUser user)
        {
            UserEntity Newuser = new UserEntity { email = user.email, password = user.password, name = user.name };

            _dbcontext.Add(Newuser);
            await _dbcontext.SaveChangesAsync();
            Console.WriteLine("shemodis");
            return true;

        }

        public async Task<bool> Delete(UserDeleteCommand id)
        {
            var user = _dbcontext.User.Find(id.Id);  
            if (user == null)
            {
                return false;
            }

            _dbcontext.User.Remove(user);
            await _dbcontext.SaveChangesAsync();

            return true;
        }

        public  async Task<List<UserModel>> FindAll()
        {
            List<UserModel> users = await (from Users in _dbcontext.User
                                select new UserModel(Users)
                                ).ToListAsync();
            return users;
           
        }

        public  async Task<UserModel> FindOne(FindOneUserCommand id)
        {
            UserModel? users = await (from Users in _dbcontext.User
                               where Users.Id == id.Id  
                               select new UserModel(Users)).FirstOrDefaultAsync();

            if (users == null) { throw new Exception("user dose not exsists"); }

            return users;
        }

        public async Task<bool> Update(UpdateUserCommand userCommand)
        {
            Guid id = userCommand.Id;

            FindOneUserCommand findOneUserCommand =  new FindOneUserCommand { Id = id };

            UserModel User = await FindOne(findOneUserCommand);

            _dbcontext.Update(User);
            await _dbcontext.SaveChangesAsync();
            return true;
        }
        public  UserModel findByEmail(string email) 
        {
            UserModel? user =  (from  Users in _dbcontext.User
                               where Users.email == email
                               select new UserModel(Users)).FirstOrDefault();
            if(user == null) { throw new Exception("email not found"); }

            return user;
        }
    }
}
