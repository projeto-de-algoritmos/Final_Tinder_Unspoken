using System.Collections.Generic;

namespace Domain.Models
{
    public class Preference:Register
    {
        public int UserId { get; set; }
        public  Dictionary<string,int> Preferencies { get; set;} = new Dictionary<string, int>()
        {
            {"Bar",0},
            {"Show",0},
            {"Cinema",0},
            {"Casa",0},
            {"Restaurante",0},
        };

        public Preference(int userId, Dictionary<string, int> preferencies)
        {
            UserId = userId;
            Preferencies = preferencies;
        }
    }
}


