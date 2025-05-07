using Ecom.Core.Entities;
using Ecom.Core.Entities.Orders;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace Ecom.Infrastructure.Repositories
{
    public class PaymentService : IPaymentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBasketRepository _basketRepository;
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;

        public PaymentService(DataContext context, IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _context = context;
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        public async Task<CustomerBasket> CreateOrUpdatePayment(Guid basketId)
        {
            StripeConfiguration.ApiKey = _configuration["StripSettings:SecretKey"];
            var basket = await _unitOfWork.BasketRepository.GetBasketAsync(basketId);
            var shippingPrice = 0m;

            if (basket == null) return null;

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliverMethod = await _context.DeliveryMethods.Where(x => x.Id == basket.DeliveryMethodId.Value).FirstOrDefaultAsync();
                shippingPrice = deliverMethod.Price;
            }

            foreach (var item in basket.BasketItems)
            {
                var productItem = await _unitOfWork.ProductRepository.GetByIdAsync(item.Id);
                if (item.Price != productItem.Price)
                {
                    item.Price = productItem.Price;
                }
            }
            var service = new PaymentIntentService();
            PaymentIntent intent;
            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var option = new PaymentIntentCreateOptions
                {
                    Amount = (long)Math.Round(basket.BasketItems.Sum(x => x.Quantity * x.Price) * 100) + (long)Math.Round(shippingPrice * 100),
                    Currency = "usd",
                    PaymentMethodTypes = new List<string>
                        {
                            "card"
                        }
                };
                intent = await service.CreateAsync(option);
                basket.PaymentIntentId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                // update
                var option = new PaymentIntentUpdateOptions
                {
                    Amount = (long)Math.Round(basket.BasketItems.Sum(x => x.Quantity * x.Price) * 100) + (long)Math.Round(shippingPrice * 100),
                };
                await service.UpdateAsync(basket.PaymentIntentId, option);

            }

            await _unitOfWork.BasketRepository.UpdateBasketAsync(basket);
            return basket;
        }

        public async Task<Order> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var order = _context.Orders.Where(x => x.PaymentIntentId == paymentIntentId).FirstOrDefault();
            if (order == null) return null;

            order.OrderStatus = OrderStatus.PaymentFailed;
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<Order> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var order = _context.Orders.Where(x => x.PaymentIntentId == paymentIntentId).FirstOrDefault();
            if (order == null) return null;

            order.OrderStatus = OrderStatus.PaymentReceived;
            await _context.SaveChangesAsync();

            return order;
        }
    }

}

