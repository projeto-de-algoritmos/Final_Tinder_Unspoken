using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Application.DTOs.Input;
using Domain.Models;
using Infra.Storage;

namespace Application.Services
{
    public class UserService
    {
        private readonly JsonStorage<User> _jsonUserStorage;
        private readonly JsonStorage<Preference> _jsonPreferenceStorage;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        public UserService(JsonStorage<User> jsonUserStorage,JsonStorage<Preference> jsonPreferenceStorage)
        {
            _jsonUserStorage = jsonUserStorage;
            _jsonPreferenceStorage = jsonPreferenceStorage;
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
        }

        public async Task CreateUser(CreateUserInputModel createUserInputModel)
        {
            try
            {
                User createdUser = await _jsonUserStorage.CreateAsync(
                    new User(
                        createUserInputModel.Email,
                        createUserInputModel.FriendsList
                    )); 

                createUserInputModel.Preferences.UserId=createdUser.Id; 
                await _jsonPreferenceStorage.CreateAsync
                    (
                        createUserInputModel.Preferences
                    );
                await _jsonUserStorage.SaveAsync();
                await _jsonPreferenceStorage.SaveAsync();
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
    }
}