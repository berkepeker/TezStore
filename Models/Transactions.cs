using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TezStore.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        // İlişki: Hangi kullanıcı
        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        // İlişki: Hangi öğe
        [Required]
        public int SellableItemId { get; set; }

        [ForeignKey(nameof(SellableItemId))]
        public SellableItem SellableItem { get; set; } = null!;

        // Tutar ve bakiye
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }       // Ödenen fiyat

        [Column(TypeName = "decimal(18,2)")]
        public decimal BalanceAfter { get; set; } // İşlem sonrası bakiye

        public DateTime TransactionDate { get; set; } = DateTime.Now;
        
        public Guid TransactionGroupId { get; set; } = Guid.NewGuid();

    }
}
