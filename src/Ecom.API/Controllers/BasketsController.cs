using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ecom.Core.Entities;
using Ecom.Core.Dtos;
using AutoMapper;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BasketsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("get-basket-items-by-basketId/{basketId}")]
        public async Task<IActionResult> GetBasketItemsById([FromRoute] Guid basketId)
        {
            var basket = await _unitOfWork.BasketRepository.GetBasketAsync(basketId);
            return Ok(basket ?? new CustomerBasket(basketId));
        }

        [HttpPost("update-basket")]
        public async Task<IActionResult> UpdateBasket([FromBody] CustomerBasketDto customerBasketDto)
        {            
            if (customerBasketDto == null)
            {
                return BadRequest("Invalid basket");
            }

            var customerBasket = _mapper.Map<CustomerBasket>(customerBasketDto);
            if (customerBasket.Id == Guid.Empty)
                customerBasket.Id = Guid.NewGuid();
            var updatedBasket = await _unitOfWork.BasketRepository.UpdateBasketAsync(customerBasket);
            if (updatedBasket == null)
            {
                return BadRequest("Failed to update basket");
            }
            return Ok(updatedBasket);
        }

        [HttpDelete("delete-basket/{basketId}")]
        public async Task<IActionResult> DeleteBasket(Guid basketId)
        {
            var deleted = await _unitOfWork.BasketRepository.DeleteBasketAsync(basketId);
            if (!deleted)
            {
                return NotFound("Basket not found");
            }
            return NoContent();
        }

    }

}
