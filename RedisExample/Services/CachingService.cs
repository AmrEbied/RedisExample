 
using StackExchange.Redis;
using System.Text.Json;

namespace RedisExample.Services
{
    internal class CachingService : ICachingService
    {
        IDatabase _cacheDB; 
        private readonly ConnectionMultiplexer _redisConnection;
        public CachingService()
        { 
            var redisConfiguration = new ConfigurationOptions
            {
                EndPoints = { "localhost:6379" },
                AbortOnConnectFail = false
            };

            _redisConnection = ConnectionMultiplexer.Connect(redisConfiguration); 
            _cacheDB = _redisConnection.GetDatabase();
        }
        public T GetData<T>(string key)
        {
            string value = _cacheDB.StringGet(key);
            if(!string.IsNullOrEmpty(value))
            {
                
                return JsonSerializer.Deserialize<T>(value);
            } 
            return default;
        }

        public object RemoveData(string key)
        {
         var exist=_cacheDB.KeyExists(key);
            if(exist)
                return _cacheDB.KeyDelete(key);

            return exist;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expireTime = expirationTime.DateTime.Subtract(DateTime.UtcNow);
            return _cacheDB.StringSet(key, JsonSerializer.Serialize(value), expireTime);
        }
    }
}
