using Ecom.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Dtos
{
    public class OrderDto
    {
        public Guid BasketId { get; set; }
        public Guid DeliveryMethodId { get; set; }
        public AddressDto ShipToAddress { get; set; }
    }
}
