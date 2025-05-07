using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Ecom.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("get-all-categories")]
        public async Task<IActionResult> Get()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            if (categories != null)
            {
                var allCategories = _mapper.Map<IReadOnlyList<Category>, IReadOnlyList<ListingCategoryDto>>(categories);
                return Ok(allCategories);
            }
            return BadRequest("Not Found");
        }

        [HttpGet("get-category-by-id/{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category != null)
            {
                var categoryDto = _mapper.Map<Category, ListingCategoryDto>(category);
                return Ok(categoryDto);
            }
            return BadRequest($"Not Found this [{id}]");
        }

        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newCategory = _mapper.Map<Category>(categoryDto);

                    await _unitOfWork.CategoryRepository.AddAsync(newCategory);
                    if (newCategory != null)
                        return Ok(categoryDto);
                    return BadRequest("Failed to add new category");
                }

                return BadRequest("Invalid Model");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("update-category")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto categoryDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingCategory = await _unitOfWork.CategoryRepository.GetByIdAsync(categoryDto.Id);
                    if (existingCategory != null)
                    {
                        existingCategory = _mapper.Map<Category>(categoryDto);
                        await _unitOfWork.CategoryRepository.UpdateAsync(categoryDto.Id, existingCategory);
                        return Ok(categoryDto);
                    }
                    return BadRequest($"Category Not Found, [{categoryDto.Id}] incorrect");
                }
                return BadRequest("Invalid Model");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
                if (category != null)
                {
                    await _unitOfWork.CategoryRepository.DeleteAsync(id);
                    return Ok($"[{category.Name}] Category Deleted Successfully");
                }
                return BadRequest($"Category Not Found, [{id}] incorrect");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
