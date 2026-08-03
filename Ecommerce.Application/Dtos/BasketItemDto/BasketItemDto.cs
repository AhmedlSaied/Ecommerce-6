using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Dtos.BasketItemDto
{
    public class BasketItemDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "Product name must be between 2 and 100 characters.")]
        public string ProductName { get; set; } = default!;

        [Required(ErrorMessage = "Picture URL is required.")]
        [Url(ErrorMessage = "Please enter a valid URL.")]
        public string PictureUrl { get; set; } = default!;

        [Range(0.01, 1000000,
            ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Range(1, 100,
            ErrorMessage = "Quantity must be between 1 and 100.")]
        public int Quantity { get; set; }
    }
}
