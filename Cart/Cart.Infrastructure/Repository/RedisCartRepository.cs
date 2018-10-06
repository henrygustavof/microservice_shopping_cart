namespace Cart.Infrastructure.Repository
{
    using Cart.Domain.Repository;
    using Cart.Domain.Entity;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using StackExchange.Redis;
    using System.Linq;
    using Microsoft.Extensions.Logging;

    public class RedisCartRepository : ICartRepository
    {
        private readonly ILogger<RedisCartRepository> _logger;

        private readonly ConnectionMultiplexer _redis;
        private readonly IDatabase _database;

        public RedisCartRepository(ILoggerFactory loggerFactory, ConnectionMultiplexer redis)
        {
            _logger = loggerFactory.CreateLogger<RedisCartRepository>();
            _redis = redis;
            _database = redis.GetDatabase();
        }

        public async Task<bool> DeleteCartAsync(string id)
        {
            return await _database.KeyDeleteAsync(id);
        }

        public IEnumerable<string> GetUsers()
        {
            var server = GetServer();
            var data = server.Keys();
            return data?.Select(k => k.ToString());
        }

        public async Task<Cart> GetCartAsync(string buyerId)
        {
            var data = await _database.StringGetAsync(buyerId);

            if (data.IsNullOrEmpty)
            {
                return new Cart { BuyerId = buyerId, Items = new List<CartItem>() };
            }

            return JsonConvert.DeserializeObject<Cart>(data);
        }

        public async Task<Cart> UpdateCartAsync(Cart basket)
        {
            var created = await _database.StringSetAsync(basket.BuyerId, JsonConvert.SerializeObject(basket));
            if (!created)
            {
                _logger.LogInformation("Problem occured persisting the item.");
                return null;
            }

            _logger.LogInformation("Basket item persisted succesfully.");

            return await GetCartAsync(basket.BuyerId);
        }

        private IServer GetServer()
        {
            var endpoint = _redis.GetEndPoints();
            return _redis.GetServer(endpoint.First());
        }

    }
}
