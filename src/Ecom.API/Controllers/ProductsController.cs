using AutoMapper;
using Ecom.API.Errors;
using Ecom.Core.Dtos;
using Ecom.Core.Entities;
using Ecom.Core.Helper;
using Ecom.Core.Interfaces;
using Ecom.Core.Sharing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("get-all-products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseCommonResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<ProductDto>>> GetAllProducts([FromQuery] ProductParams productParams)
        {
            if (productParams.PageNumber <= 0) return BadRequest(new BaseCommonResponse(400, "Invalid parameters"));

            var allProducts = await _unitOfWork.ProductRepository.GetAllAsync(productParams);

            //mapping to ResponseProductDto
            var responseProductDtoList = _mapper.Map<Pagination<ProductDto>>(allProducts);

            // Return an OK response with paginated product data
            return Ok(responseProductDtoList);
            //return Ok(new Pagination<ProductDto>(productParams.PageNumber, productParams.PageSize, totalCount, responseProductDtoList));
        }

        [HttpGet("get-product-by-id/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseCommonResponse),StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id, x => x.Category);
            if (product != null)
            {
                var productDto = _mapper.Map<ProductDto>(product);
                return Ok(productDto);
            }
            return NotFound(new BaseCommonResponse(404));
        }

        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductDto createProductDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var newProduct = _mapper.Map<Product>(createProductDto);
                    var res = await _unitOfWork.ProductRepository.AddAsync(createProductDto);

                    return res ? Ok(createProductDto) : BadRequest("Failed to add new product");
                }
                return BadRequest("Invalid Model");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-product/{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromForm] UpdateProductDto updateProductDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
                    if (product == null)
                    {
                        return BadRequest($"Not Found this [{id}]");
                    }
                    var res = await _unitOfWork.ProductRepository.UpdateAsync(id, updateProductDto);
                    return res != null ? Ok(_mapper.Map<Product>(updateProductDto)) : BadRequest("Failed to update product");
                }
                return BadRequest("Invalid Model");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-product/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return BadRequest($"Not Found this [{id}]");
                }
                var res = await _unitOfWork.ProductRepository.DeleteWithPictureAsync(id);
                return res ? Ok("Product Deleted") : BadRequest("Failed to delete product");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
