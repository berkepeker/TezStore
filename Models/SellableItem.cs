using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;               // [Required] için
using System.ComponentModel.DataAnnotations.Schema;        // [Column] için

namespace TezStore.Models
{
    public class SellableItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? ImagePath { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
