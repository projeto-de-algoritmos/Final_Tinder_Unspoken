using Domain.Models;

namespace Infra.Storage
{
    public class JsonStorageOptions<T> where T : Register
    {
        public string FilePath { get; set; } 

        public JsonStorageOptions()
        {
            FilePath = "/home/user/Documentos/UnB/2021.2/PA/Final/Final_Tinder_Unspoken/Infra/Storage/User.json";
            if(typeof(T) == typeof(Preference))
                FilePath = "/home/user/Documentos/UnB/2021.2/PA/Final/Final_Tinder_Unspoken/Infra/Storage/Preference.json";
            
        }
    }
}