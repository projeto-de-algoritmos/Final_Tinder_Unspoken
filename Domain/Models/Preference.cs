using System.Collections.Generic;

namespace Domain.Models
{
    public class Preference
    {
        public  Dictionary<string,int> Fun { get; set;} = new Dictionary<string, int>()
        {
            {"Bar",0},
            {"Show",0},
            {"Cinema",0},
            {"Casa",0},
            {"Restaurante",0},
        };
        public  Dictionary<string,int> Hobbie { get; set; } = new Dictionary<string, int>()
        {
            {"Ler",0},
            {"Filmes",0},
            {"Series",0},
            {"Esportes",0},
        };        
    }
}


