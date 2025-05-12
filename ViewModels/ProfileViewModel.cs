using System;
using System.Collections.Generic;
using TezStore.Models;

namespace TezStore.ViewModels
{
    public class ProfileViewModel
    {
        public User User { get; set; }
        public List<Transaction> StoreTransactions { get; set; }
        public List<RecyclingTransaction> RecyclingTransactions { get; set; }
        public List<IGrouping<Guid, Transaction>> GroupedStoreTransactions { get; set; } = new();
    }

    public class UserProfileEditViewModel
    {
        public string TelefonNo { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Cinsiyet { get; set; }
        public DateTime? DogumTarihi { get; set; }

        // Şifre değişikliği için
        public string? MevcutSifre { get; set; }
        public string? YeniSifre { get; set; }
        public string? YeniSifreTekrar { get; set; }
    }
}
