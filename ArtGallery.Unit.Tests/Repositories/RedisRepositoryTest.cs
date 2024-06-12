using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArtGallery.Models;
using StackExchange.Redis;

namespace ArtGallery.Unit.Tests.Repositories {
	[TestFixture]
	internal class RedisRepositoryTest {

		private async Task<IConnectionMultiplexer> CreateRedisDatabase() {
			IConnectionMultiplexer database = await ConnectionMultiplexer.ConnectAsync("localhost");
			return database;
		}

		[SetUp]
		public async Task SetUp() {
			var redis = await CreateRedisDatabase();
			await redis.GetDatabase().ExecuteAsync("FLUSHDB");
		}

		[Test]
		public async Task RedisCacheStoreTest() {
			var redis = await this.CreateRedisDatabase();
			var _repository = new RedisRepository(redis);
			string data = "In numbers pure, equations dance, Code weaves logic's fine romance. Digits, symbols, lines of light, Crafting dreams in endless night.";
			var store = await _repository.Store("test", data);
			Assert.That(store, Is.EqualTo(true));
		}

		[Test]
		public async Task RedisCacheGetTest() {
			var redis = await this.CreateRedisDatabase();
			var _repository = new RedisRepository(redis);
			var data = new Period() { Name = "Test", Summary = "Testando" };
			var store = await _repository.Store("test", new List<Period>() { data });
			Assert.That(store, Is.EqualTo(true));
			var get = await _repository.Get<Period>("test");
			Assert.That(get, Is.Not.Null);
			Assert.That(get.Count, Is.EqualTo(1));
		}
	}
}
