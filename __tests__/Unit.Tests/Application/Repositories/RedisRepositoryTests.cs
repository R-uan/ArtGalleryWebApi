using StackExchange.Redis;
using System.Text.Json;
using ArtGallery.Application.Repositories;


namespace Unit.Tests.Repositories
{
    [TestFixture]
    public class RedisRepositoryTests
    {
        private static ConnectionMultiplexer GetRedisDatabaseConnection()
        {
            return ConnectionMultiplexer.Connect("127.0.0.1");
        }

        [Test]
        public void RedisConnectionTest()
        {
            var connect = GetRedisDatabaseConnection();
            Assert.That(connect.IsConnected, Is.True);
        }

        [Test]
        public async Task StoreKeyTest()
        {
            using (var connection = GetRedisDatabaseConnection())
            {
                RedisRepository repository = new(connection);
                var store = await repository.Store("test-string", "string-teste");
                Assert.That(store, Is.True);
            }

            using (var connection = GetRedisDatabaseConnection())
            {
                RedisRepository repository = new(connection);
                var store = await repository.Store("test-list", new List<string>() { "Hello", "World!" });
                Assert.That(store, Is.True);
            }
        }

        [Test]
        public async Task GetKeyTest()
        {
            using (var connection = GetRedisDatabaseConnection())
            {
                //
                //  Set key for retrieve
                await connection.GetDatabase().StringSetAsync("test-string", JsonSerializer.Serialize("string-test"));
                //
                //  Get key
                RedisRepository repository = new(connection);
                var get = await repository.Get<string>("test-string");
                Assert.That(get, Is.Not.Null);
                Assert.That(get, Is.TypeOf<string>());
            }

            using (var connection = GetRedisDatabaseConnection())
            {
                //
                //  Set key for retrieve
                await connection.GetDatabase().StringSetAsync("test-list", JsonSerializer.Serialize(new List<string>() { "Hello", "World!" }));
                //
                //  Get key
                RedisRepository repository = new(connection);
                var get = await repository.Get<List<string>>("test-list");
                Assert.That(get, Is.Not.Null);
                Assert.That(get, Is.TypeOf<List<string>>());
            }
        }

        [Test]
        public async Task GetNonExistentKeyTest()
        {
            using (var connection = GetRedisDatabaseConnection())
            {
                RedisRepository repository = new(connection);
                var get = await repository.Get<string>("should-return-null");
                Assert.That(get, Is.Null);
            }
        }
    }
}
