using TezStore.Models;

namespace TezStore.ViewModels
{
    public class AddUserViewModel
    {
        public User NewUser { get; set; } = new User
        {
            TelefonNo = "",
            Sifre = "",
            Ad = "",
            Soyad = "",
            Cinsiyet = "Erkek"
        };

        public List<User> ExistingUsers { get; set; } = new List<User>();
    }
}
