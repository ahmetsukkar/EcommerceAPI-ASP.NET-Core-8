using Ecom.API.Errors;
using Ecom.Core.Entities;
using Ecom.Core.Entities.Orders;
using Ecom.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentsController> _logger;
        private const string endpointSecret = "whsec_d3130cfff2cc06214ea58d12dc411119171da0fce43abe275ff015cd8454d8fb";


        public PaymentsController(IPaymentService paymentService, ILogger<PaymentsController> logger)
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent(Guid basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePayment(basketId);
            if (basket == null) return BadRequest(new BaseCommonResponse(400, "Problem with your basket"));
            return Ok(basket);
        }

        [HttpPost("webhook")]
        public async Task<IActionResult> StripeWebhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret,
                    throwOnApiVersionMismatch: false); // ✅ This line avoids the exception

                PaymentIntent intent;
                Order order;

                switch (stripeEvent.Type)
                {
                    case "payment_intent.payment_failed":
                        intent = (PaymentIntent)stripeEvent.Data.Object;
                        _logger.LogInformation("Payment failed with ID: {PaymentId}", intent.Id);
                        order = await _paymentService.UpdateOrderPaymentFailed(intent.Id);
                        _logger.LogInformation("Updated order status to Failed for Order ID: {OrderId}", order.Id);
                        break;

                    case "payment_intent.succeeded":
                        intent = (PaymentIntent)stripeEvent.Data.Object;
                        _logger.LogInformation("Payment succeeded with ID: {PaymentId}", intent.Id);
                        order = await _paymentService.UpdateOrderPaymentSucceeded(intent.Id);
                        _logger.LogInformation("Updated order status to Success for Order ID: {OrderId}", order.Id);
                        break;
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
