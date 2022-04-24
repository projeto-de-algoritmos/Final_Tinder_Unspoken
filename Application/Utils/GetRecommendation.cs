using Application.DTOs.View;
using Domain.Models;
using Infra.Storage;

namespace Application.Utils
{
    public class GetRecommendation
    {
        private readonly JsonStorage<User> _jsonUserStorage;
        private readonly JsonStorage<Preference> _jsonPreferenceStorage;
        private readonly InversionCounter _inversionCounter;
        public Dictionary<int,int> Results = new Dictionary<int, int>();

        public GetRecommendation(JsonStorage<User> jsonUserStorage, JsonStorage<Preference> jsonPreferenceStorage, InversionCounter inversionCounter)
        {
            _jsonUserStorage = jsonUserStorage;
            _jsonPreferenceStorage = jsonPreferenceStorage;
            _inversionCounter = inversionCounter;
        }

        public async Task<GetRecommendationViewModel> GetRecommendations(User newFriend)
        {
            Preference newFriendPreference = await GetPreference(newFriend.Id);
            Dictionary<string,int> newFriendPreferenceOrdered = newFriendPreference.Preferencies
                    .OrderBy(pr=>pr.Value)
                    .ToDictionary(x => x.Key, x => x.Value);

            foreach (int friendId in newFriend.FriendsList)
            {
                User friend = await _jsonUserStorage.GetByIdAsync(friendId);
                Preference friendPreference = await GetPreference(friend.Id);
                Dictionary<string,int> friendPreferenceOrdered = OrderPreferenceByReference(friendPreference.Preferencies,newFriendPreferenceOrdered);
                FillResult(friendId,friendPreferenceOrdered);
            }
            int minInversions = Results.Values.Min();

            return new GetRecommendationViewModel
            ( 
                Results.Keys
                    .Where(ky => Results[ky] == minInversions)
                    .ToList()
            );
        }

        private void FillResult(int friendId,Dictionary<string, int> friendPreferenceOrdered)
        {
            _inversionCounter.GetInversions(friendPreferenceOrdered.Values.ToArray());
            int inversions = _inversionCounter.Inversions;
            Results.Add(friendId,inversions);
        }

        private async Task<Preference> GetPreference(int id)
        {
            return (await _jsonPreferenceStorage.GetAllAsync())
                .First
                (
                        pr => pr.UserId == id 
                );
        }

        private Dictionary<string, int> OrderPreferenceByReference(Dictionary<string, int> preferencies, Dictionary<string, int> referencie)
        {
            Dictionary<string, int> newDict = new Dictionary<string, int>();
            foreach (string key in referencie.Keys)
            {
                newDict.Add(key,preferencies[key]);
            }
            return newDict;
        }
    
        
    }
}