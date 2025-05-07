using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Infrastructure.Data;
using Microsoft.Extensions.FileProviders;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;
        private readonly IConnectionMultiplexer _redis;

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IBasketRepository BasketRepository { get; }

        public UnitOfWork(DataContext dataContext, IFileProvider fileProvider, IMapper mapper, IConnectionMultiplexer redis)
        {
            _dataContext = dataContext;
            _fileProvider = fileProvider;
            _mapper = mapper;
            _redis = redis;
            CategoryRepository = new CategoryRepository(_dataContext);
            ProductRepository = new ProductRepository(_dataContext, fileProvider, mapper);
            BasketRepository = new BasketRepository(_redis,_mapper);
        }
    }
}
