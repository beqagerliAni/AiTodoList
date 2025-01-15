using Microsoft.AspNetCore.Mvc;
using To_do_List.src.Modules.User.Entity;
using To_do_List.src.Modules.User.Model;
using todolist.Helper.Auth.Command;
using todolist.Helper.Jwt;
using todolist.src.Modules.User.Repository;
namespace todolist.Helper.Auth.Service
{
    public class AuthService : IAuthService
    {
        IUserRepository _userRepository;
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> Login(LoginCommand command)
        {
            UserModel user =   _userRepository.findByEmail(command.email); 
            
            if(user != null && BCrypt.Net.BCrypt.Verify(command.password,user.password))
            {
                if(user.Guuid ==null) { throw new Exception("asdsad"); }

                Guid guid = (Guid)user.Guuid;

                string token = await JwtGenerate.GenerateJwtAsync(1, guid);

                return token;
            }

            throw new Exception("user not found");
        }

        public async Task<bool> Register(RegisterCommand command)
        {
            command.password = BCrypt.Net.BCrypt.HashPassword(command.password);

            UserEntity newUser = new UserEntity {  email = command.email, name= command.name, password = command.password};

            return  await _userRepository.Create(command);

        }
    }
}
