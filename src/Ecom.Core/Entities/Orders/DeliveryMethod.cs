namespace Ecom.Core.Entities.Orders
{
    public class DeliveryMethod:BaseEntity<Guid>
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod(string shortName, string deliveryTime, decimal price, string description)
        {
            ShortName = shortName;
            DeliveryTime = deliveryTime;
            Price = price;
            Description = description;
        }

        public string ShortName { get; set; }
        public string DeliveryTime { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }
}