namespace Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public List<User> FriendsList { get; set; }
        public Preference Preferences { get; set; }
        public DateTime CreatedAt { get; set; }
        
        

        public User(string email, List<User> friendsList, Preference preferences)
        {
            Email = email;
            FriendsList = friendsList;
            Preferences = preferences;
        }
    }
}