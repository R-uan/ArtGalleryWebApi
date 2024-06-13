using System.Collections;
using System.Text.Json;
using StackExchange.Redis;

namespace ArtGallery;

public class RedisRepository(IConnectionMultiplexer muxer) : IRedisRepository {
	private readonly IDatabase _redis = muxer.GetDatabase();

	public async Task<bool> Store<T>(string key, T values) {
		string serialize = JsonSerializer.Serialize(values);
		var store = await _redis.StringSetAsync(key, serialize);
		return store;
	}

	public async Task<T?> Get<T>(string key) {
		string? cache = await _redis.StringGetAsync(key);
		if (cache == null) return default;
		var data = JsonSerializer.Deserialize<T>(cache);
		return data;
	}
}
