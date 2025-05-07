using Ecom.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Services
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(string buyerEmail, Guid deliveryMethodId, Guid basketId, ShipAddress shipAddress);
        Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail);
        Task<Order> GetOrderByIdAsync(Guid id, string buyerEmail);
        Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync();
        //Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail, int pageIndex, int pageSize);
        //Task<int> GetOrdersCountForUserAsync(string buyerEmail);
    }
}
