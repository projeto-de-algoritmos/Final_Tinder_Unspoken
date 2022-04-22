namespace Domain.Models
{
    public class User:Register
    {
        public string Email { get; set; }
        public List<User> FriendsList { get; set; }
        public User(string email, List<User> friendsList)
        {
            Email = email;
            FriendsList = friendsList;
        }
    }
}