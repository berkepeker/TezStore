using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TezStore.Models
{
    public class RecyclingTransaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string ProductId { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal PointsEarned { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal NewBalance { get; set; }

        public DateTime IslemTarihi { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
    }
}
