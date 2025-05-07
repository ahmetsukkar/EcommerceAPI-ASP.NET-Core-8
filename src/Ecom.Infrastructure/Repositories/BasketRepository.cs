using AutoMapper;
using Ecom.Core.Dtos;
using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDatabase _database;
        private readonly IMapper _mapper;

        public BasketRepository(IConnectionMultiplexer redis, IMapper mapper)
        {
            _database = redis.GetDatabase();
            _mapper = mapper;
        }

        public async Task<bool> DeleteBasketAsync(Guid basketId)
        {
            var check = await _database.KeyExistsAsync(basketId.ToString());
            if (check)
                return await _database.KeyDeleteAsync(basketId.ToString());
            return false;
        }
        public async Task<CustomerBasket> GetBasketAsync(Guid basketId)
        {
            var data = await _database.StringGetAsync(basketId.ToString());

            return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CustomerBasket>(data);
        }
        public async Task<CustomerBasket> UpdateBasketAsync(CustomerBasket CustomerBasket)
        {
            var _basket = await _database.StringSetAsync(CustomerBasket.Id.ToString(),
                                JsonSerializer.Serialize<CustomerBasket>(CustomerBasket),
                                TimeSpan.FromDays(30));

            if (!_basket) return null;
            return await GetBasketAsync(CustomerBasket.Id);
        }
    }

}
