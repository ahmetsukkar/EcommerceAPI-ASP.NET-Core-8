using Ecom.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Ecom.Core.Dtos
{
    public class BaseProductDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(1,9999, ErrorMessage = "Price limited by {0} and {1}")]
        [RegularExpression(@"^\d+\.\d{0,2}$", ErrorMessage = "Price must be a decimal with 2 decimal places")]
        public decimal Price { get; set; }
    }

    public class ProductDto: BaseProductDto
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string Picture { get; set; }
    }

    public class CreateProductDto: BaseProductDto
    {
        public Guid CategoryId { get; set; }
        public IFormFile Image { get; set; }
    }

    public class UpdateProductDto : BaseProductDto
    {
        public Guid CategoryId { get; set; }
        public string OldPicture { get; set; }
        public IFormFile Picture { get; set; }
    }
}
