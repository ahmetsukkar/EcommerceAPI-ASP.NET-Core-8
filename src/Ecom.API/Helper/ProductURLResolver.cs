using AutoMapper;
using AutoMapper.Execution;
using Ecom.Core.Dtos;
using Ecom.Core.Entities;

namespace Ecom.API.Helper
{
    public class ProductURLResolver :IValueResolver<Product, ProductDto, string>
    {
        private readonly IConfiguration _configuration;

        public ProductURLResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.Picture))
            {
                return $"{_configuration["PictureServer:Url"]}{source.Picture}";
            }
            return null;
        }
    }
}
