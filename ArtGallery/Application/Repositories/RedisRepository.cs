using System.Collections;
using System.Reflection;
using System.Text.Json;
using ArtGallery.Utils.Caching;
using StackExchange.Redis;

namespace ArtGallery;

public class RedisRepository(IConnectionMultiplexer muxer) : IRedisRepository
{
	private readonly IDatabase _redis = muxer.GetDatabase();

	public async Task<bool> Store<T>(string key, T values)
	{
		string serialize = JsonSerializer.Serialize(values);
		TimeSpan expiration = TimeSpan.FromMinutes(50);
		var store = await _redis.StringSetAsync(key, serialize, expiration);
		return store;
	}

	public async Task<T?> Get<T>(string key)
	{
		string? cache = await _redis.StringGetAsync(key);
		if (cache == null) return default;
		var data = JsonSerializer.Deserialize<T>(cache);
		return data;
	}

	public void ClearThisKeys(MethodBase method)
	{
		var prefix = CacheKeyHelper.GenerateCacheKey(method).Split('+')[0];
		var server = muxer.GetServer(muxer.GetEndPoints()[0]);
		var keys = server.Keys(database: 0, pattern: $"{prefix}*");
		foreach (var key in keys)
		{
			_redis.KeyDelete(key);
		}
	}
}
