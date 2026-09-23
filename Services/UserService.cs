using Apps.Dtos.Users;
using Apps.Repository;
using Microsoft.AspNetCore.Mvc;
using Apps.Entities;
namespace Apps.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task StoreUser(CreateUserDto createUserDto) { 
                
            await _userRepository.StoreUser(createUserDto);
        
        }

        public async Task<User> GetUserByEmail(LoginUserDto request)
        {
           var user  = await _userRepository.GetUserByEmail(request.Email); 

            return user;
        }

    }
}
