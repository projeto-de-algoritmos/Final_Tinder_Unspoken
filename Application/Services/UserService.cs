using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using Application.DTOs.Input;
using Application.DTOs.View;
using Application.Utils;
using Domain.Models;
using Infra.Storage;

namespace Application.Services
{
    public class UserService
    {
        private readonly JsonStorage<User> _jsonUserStorage;
        private readonly JsonStorage<Preference> _jsonPreferenceStorage;
        private readonly JsonSerializerOptions _jsonSerializerOptions;
        private readonly GetRecommendation _getRecommendation;
        public UserService(JsonStorage<User> jsonUserStorage,JsonStorage<Preference> jsonPreferenceStorage,GetRecommendation getRecommendation)
        {
            _jsonUserStorage = jsonUserStorage;
            _jsonPreferenceStorage = jsonPreferenceStorage;
            _jsonSerializerOptions = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
                WriteIndented = true
            };
            _getRecommendation = getRecommendation;
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

        public async Task<GetRecommendationViewModel> AddFriend(int userId, int friendId)
        {
            try
            {
                User user = await _jsonUserStorage.GetByIdAsync(userId);
                User newFriend = await _jsonUserStorage.GetByIdAsync(friendId);
                user.FriendsList.Add(friendId);
                await _jsonUserStorage.SaveAsync();

                GetRecommendationViewModel recommendations = await _getRecommendation.GetRecommendations(newFriend);
                return recommendations;
            }
            catch (System.Exception ex)
            {
                throw;
            }
        
        }
    }
}