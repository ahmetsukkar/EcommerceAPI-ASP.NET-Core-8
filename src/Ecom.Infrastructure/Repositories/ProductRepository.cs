using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities;
using Ecom.Core.Helper;
using Ecom.Core.Interfaces;
using Ecom.Core.Sharing;
using Ecom.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;

        public ProductRepository(DataContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
        {
            _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;
        }

        public async Task<Pagination<Product>> GetAllAsync(ProductParams productParams)
        {


            var productsQuery = from product in _context.Products.Include(i => i.Category)
                                select product;

            //search by name
            if (!string.IsNullOrEmpty(productParams.SearchByProductName))
            {
                productsQuery = productsQuery.Where(f => f.Name.ToLower().Contains(productParams.SearchByProductName));
            }

            //filtering by categoryId
            if (productParams.CategoryId.HasValue)
                productsQuery = productsQuery.Where(f => f.CategoryId == productParams.CategoryId.Value);

            //sorting
            productsQuery = productParams.Sort switch
            {
                "PriceAsce" => productsQuery.OrderBy(f => f.Price),
                "PriceDesc" => productsQuery.OrderByDescending(f => f.Price),
                _ => productsQuery.OrderBy(f => f.Name),
            };

            //paging *If the version is SQL Server 2012 or later, then OFFSET-FETCH is supported.*
            //productsQuery = productsQuery.Skip((productParams.PageNumber - 1) * productParams.PageSize).Take(productParams.PageSize);

            var totalCount =  productsQuery.Count();

            productsQuery = productsQuery.Skip((productParams.PageNumber - 1) * productParams.PageSize).Take(productParams.PageSize);

            var productList = await productsQuery.ToListAsync();

            var productPagedResult = new Pagination<Product>(productParams.PageNumber, productParams.PageSize, totalCount, productList);

            return productPagedResult;
        }

        public async Task<bool> AddAsync(CreateProductDto createProductDto)
        {
            string newProductPicturePath = await UploadProductImageAsync(createProductDto.Image);

            var newProduct = _mapper.Map<Product>(createProductDto);
            if (newProductPicturePath != null)
                newProduct.Picture = newProductPicturePath;
            await _context.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Product> UpdateAsync(Guid id, UpdateProductDto updateProductDto)
        {
            var currentProduct = await _context.Products.FindAsync(id);
            if (currentProduct == null) return null;

            string newProductPicturePath = await UploadProductImageAsync(updateProductDto.Picture);

            //remove old picture
            DeleteOldProductImageAsync(currentProduct.Picture);

            currentProduct.Name = updateProductDto.Name;
            currentProduct.Description = updateProductDto.Description;
            currentProduct.Price = updateProductDto.Price;
            currentProduct.CategoryId = updateProductDto.CategoryId;
            currentProduct.Picture = newProductPicturePath;

            _context.Products.Update(currentProduct);
            await _context.SaveChangesAsync();
            return currentProduct;
        }

        private async Task<string> UploadProductImageAsync(IFormFile image)
        {
            if (image != null)
            {
                var productDirectory = "/images/products/";
                var productName = String.Format("{0}-{1}", Guid.NewGuid(), image.FileName);
                if (!Directory.Exists("wwwroot" + productDirectory))
                {
                    Directory.CreateDirectory("wwwroot" + productDirectory);
                }
                var src = productDirectory + productName;
                var picInfo = _fileProvider.GetFileInfo(src);
                var rootPath = picInfo.PhysicalPath;
                using (var fileStream = new FileStream(rootPath, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                return src;
            }

            return null;
        }

        private void DeleteOldProductImageAsync(string currentProductPicture)
        {
            if (!string.IsNullOrEmpty(currentProductPicture))
            {
                var pictureInfo = _fileProvider.GetFileInfo(currentProductPicture);
                var rootpath = pictureInfo.PhysicalPath;
                System.IO.File.Delete(rootpath);
            }
        }

        public async Task<bool> DeleteWithPictureAsync(Guid id)
        {
            var currentProduct = await _context.Products.FindAsync(id);
            if (currentProduct != null)
            {
                _context.Products.Remove(currentProduct);
                _context.SaveChanges();

                //remove picture
                DeleteOldProductImageAsync(currentProduct.Picture);

                return true;
            }

            return false;
        }
    }
}
