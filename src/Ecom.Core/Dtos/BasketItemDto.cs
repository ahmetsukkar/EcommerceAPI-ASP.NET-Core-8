using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public string ProductPicture { get; set; }

        [Required]
        [Range(0.1, double.MaxValue, ErrorMessage = "Price Must Greater than Zero")]
        public decimal Price { get; set; }

        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Quantity Must Greater than Zero")]
        public int Quantity { get; set; }

        [Required]
        public string Category { get; set; }
    }
}
