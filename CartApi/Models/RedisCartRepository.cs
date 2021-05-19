using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Common.Models
{
    public class RedisCartRepository : ICartRepository
    {
        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        public RedisCartRepository(ConnectionMultiplexer redis)
        {
            _redis = redis;
            _database = redis.GetDatabase();
        }
        public async Task<bool> DeleteCartAsync(string CartId)
        {
           return await _database.KeyDeleteAsync(CartId);

        }

        public async Task<Cart> GetCartAsync(string cartId)
        {
           var cart =  await _database.StringGetAsync(cartId);
           if(cart.IsNullOrEmpty)  //first time no cart found
            {
                return null;
            }


            return JsonConvert.DeserializeObject<Cart>(cart);   
        }

        public IEnumerable<string> GetUsers()
        {
            var server = GetServer();
            var data = server.Keys();
            return data?.Select(k => k.ToString());
        }

        private IServer GetServer()
        {
            var endpoints = _redis.GetEndPoints();
            return _redis.GetServer(endpoints.First());
        }

        public async Task<Cart> UpdatecartAsync(Cart basket)
        {
            var created = await _database.StringSetAsync(basket.BuyerId, JsonConvert.SerializeObject(basket));
            if(!created)
            {
                return null;
            }

            return await GetCartAsync(basket.BuyerId);
        }
    }
}
