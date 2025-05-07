using Ecom.Core.Entities.Orders;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly DataContext _dataContext;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork, DataContext dataContext, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _dataContext = dataContext;
            _paymentService = paymentService;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, Guid deliveryMethodId, Guid basketId, ShipAddress shipAddress)
        {
            var basket = await _unitOfWork.BasketRepository.GetBasketAsync(basketId);
            var items = new List<OrderItem>();
            foreach (var item in basket.BasketItems)
            {
                var productItem = await _unitOfWork.ProductRepository.GetByIdAsync(item.Id);
                //if (productItem == null) return false;
                var productItemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.Picture);
                var orderItem = new OrderItem(productItemOrdered, item.Price, item.Quantity);
                items.Add(orderItem);
            }

            await _dataContext.OrderItems.AddRangeAsync(items);
            await _dataContext.SaveChangesAsync();

            var deliveryMethod = await _dataContext.DeliveryMethods.Where(x => x.Id == deliveryMethodId).FirstOrDefaultAsync();

            // calculate subtotal
            var subTotal = items.Sum(x => x.Price * x.Quantity);

            //check if order exist
            var exitingOrder = await _dataContext.Orders.Where(x => x.PaymentIntentId == basket.PaymentIntentId).FirstOrDefaultAsync();
            if (exitingOrder is not null)
            {
                _dataContext.Orders.Remove(exitingOrder);
                await _paymentService.CreateOrUpdatePayment(basket.Id);
            }

            // initilize on ctor
            var order = new Order(buyerEmail, shipAddress, deliveryMethod, items, subTotal, basket.PaymentIntentId);

            // check order is not null
            if (order is null) return null;

            //adding order to Db
            await _dataContext.Orders.AddAsync(order);
            await _dataContext.SaveChangesAsync();

            //remove basket
            //await _unitOfWork.BasketRepository.DeleteBasketAsync(basketId);
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        => await _dataContext.DeliveryMethods.ToListAsync();


        public async Task<Order> GetOrderByIdAsync(Guid id, string buyerEmail)
        {
            var order = await _dataContext.Orders
                        .Where(x => x.Id == id && x.BuyerEmail == buyerEmail)
                        .Include(x => x.OrderItems)
                        .Include(x => x.DeliveryMethod)
                        .OrderByDescending(x => x.OrderDate)
                        .FirstOrDefaultAsync();

            return order;
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var orders = await _dataContext.Orders
                    .Where(x => x.BuyerEmail == buyerEmail)
                    .Include(x => x.OrderItems)
                    .Include(x => x.DeliveryMethod)
                    .OrderByDescending(x => x.OrderDate)
                    .ToListAsync();

            return orders;
        }
    }
}
