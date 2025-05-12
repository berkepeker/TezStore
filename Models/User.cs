using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TezStore.Models
{

    public class PasswordValidationViewModel
{
    public required string Sifre { get; set; }
}
    public class UserValidationViewModel
{
    public required string TelefonNo { get; set; }
}

    public class User
{
    [Key]
    public required string TelefonNo { get; set; }
    
    [Required]
    public required string Sifre { get; set; } 
    [Required]
    public required string Ad { get; set; }

    [Required]
    public required string Soyad { get; set; }

    public DateTime? DogumTarihi { get; set; }

    public required string Cinsiyet { get; set; }

    public DateTime KayitTarihi { get; set; } = DateTime.Now;

    public string Durum { get; set; } = "Aktif";

    public decimal Balance { get; set; } = 0.00m;

    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

}
















}




