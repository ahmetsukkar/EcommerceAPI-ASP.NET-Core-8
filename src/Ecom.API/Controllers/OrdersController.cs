using AutoMapper;
using Ecom.API.Errors;
using Ecom.API.Extensions;
using Ecom.Core.Dtos;
using Ecom.Core.Entities.Orders;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWorkm;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IUnitOfWork unitOfWorkm , IOrderService orderService,IMapper mapper)
        {
            _unitOfWorkm = unitOfWorkm;
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User.GetUserEmail();
            var address = _mapper.Map<ShipAddress>(orderDto.ShipToAddress);

            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId, orderDto.BasketId, address);

            if (order == null) return BadRequest(new BaseCommonResponse(400, "Error while creating order") );

            return Ok(order);

        }

        [HttpGet("get-orders-for-user")]
        public async Task<IActionResult> GetOrdersForUSer()
        {
            var email = HttpContext.User.GetUserEmail();
            var order = await _orderService.GetOrdersForUserAsync(email);
            var result = _mapper.Map<IReadOnlyList<OrderToReturnDto>>(order);
            return Ok(result);
        }

        [HttpGet("get-order-by-id/{id}")]
        public async Task<IActionResult> GetOrderById([FromRoute] Guid id)
        {
            var email = HttpContext.User.GetUserEmail();
            var order = await _orderService.GetOrderByIdAsync(id, email);  
            if (order is null) return NotFound(new BaseCommonResponse(404));
            var result = _mapper.Map<OrderToReturnDto>(order);
            return Ok(result);
        }

        [HttpGet("get-delivery-methods")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _orderService.GetDeliveryMethodsAsync();

            return Ok(deliveryMethods);
        }
    }
}
