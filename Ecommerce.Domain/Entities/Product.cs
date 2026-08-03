using Ecommerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class Product : BaseEntity<int> , ISoftDeletable
    {
        [Required]
        public string Name { get; set; } = default!;
        public string? Description { get; set; } 
        public decimal Price { get; set; }
        public string PictureUrl { get; set; } = default!;
        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public Brand Brand { get; set; } = default!;
        [ForeignKey("Type")]
        public int TypeId { get; set; }
        public ProductType Type { get; set; } = default!;
        public bool IsDeleted { get ; set ; }
        public DateTime? DeletedAtUtc { get ; set ; }
    }
}
