using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities.Orders;

namespace Ecom.API.Helper
{
    public class OrderItemUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _config;

        public OrderItemUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.productItemOrdered.PictureUrl))
            {
                return _config["PictureServer:Url"] + source.productItemOrdered.PictureUrl;
            }

            return null;
        }
    }
}
