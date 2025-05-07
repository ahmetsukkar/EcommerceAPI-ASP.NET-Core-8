using Ecom.Core.Entities.Orders;

namespace Ecom.Core.Dtos
{
    public class OrderItemDto
    {
        public Guid ProductItemId { get; set; }
        public string ProductItemName { get; set; }
        public string PictureUrl { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}