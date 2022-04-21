using Application.DTOs.Input;
using Domain.Models;
using Infra.Storage;

namespace Application.Services
{
    public class UserService
    {
        private readonly JsonStorage<User> _jsonStorage;

        public UserService(JsonStorage<User> jsonStorage)
        {
            _jsonStorage = jsonStorage;
        }

        public async Task CreateUser(CreateUserInputModel createUser)
        {
            try
            {
                await _jsonStorage.CreateAsync(new User(
                    createUser.Email,
                    createUser.FriendsList,
                    createUser.Preferences
                ));
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
    }
}