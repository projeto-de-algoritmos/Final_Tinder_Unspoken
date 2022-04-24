using Domain.Models;

namespace Application.DTOs.Input
{
    public class CreateUserInputModel
    {
        public string Email { get; set; }
        public List<int> FriendsList { get; set; }
        public Preference Preferences { get; set; }

        public CreateUserInputModel(string email, List<int> friendsList, Preference preferences)
        {
            Email = email;
            FriendsList = friendsList;
            Preferences = preferences;
        }
    }
}