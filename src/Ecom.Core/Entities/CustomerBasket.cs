using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {
            
        }
        public CustomerBasket(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();

        public Guid? DeliveryMethodId { get; set; }
        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }
        public decimal ShippingPrice { get; set; }

    }
}
