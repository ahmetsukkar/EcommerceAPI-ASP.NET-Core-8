using Ecom.Core.Helper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities;
using Ecom.Core.Sharing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface IProductRepository:IGenericRepository<Product>
    {
        Task<Pagination<Product>> GetAllAsync(ProductParams productParams);

        Task<bool> AddAsync(CreateProductDto createProductDto);

        Task<Product> UpdateAsync(Guid id, UpdateProductDto updateProductDto);

        Task<bool> DeleteWithPictureAsync(Guid id);
    }
}
