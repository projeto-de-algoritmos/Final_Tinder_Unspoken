namespace Domain.Models
{
    public class User : Register
    {
        public string Email { get; set; }
        public List<int> FriendsList { get; set; }

        public User(string email, List<int> friendsList)
        {
            Email = email;
            FriendsList = friendsList;
        }
    }
}