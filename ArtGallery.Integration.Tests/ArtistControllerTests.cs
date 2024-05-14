using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Integration.Tests {
    [TestFixture]
    public class ArtistControllerTests {
        private readonly ArtGalleryWebAppFactory _factory;
        private readonly HttpClient _client;

        public ArtistControllerTests() {
            _factory = new ArtGalleryWebAppFactory();
            _client = _factory.CreateClient();
        }

        [OneTimeSetUp]
        public async Task SetUp() {
            if (_factory == null) throw new Exception("_factory was not defined.");
            await _factory.InitializeDatabase();
        }

        [OneTimeTearDown]
        public async Task TearDown() {
            if (_factory == null) throw new Exception("_factory was not defined.");
            await _factory.DestroyDatabase();
        }

        [Test]
        public async Task Get_AllArtists_ReturnsOk_Test() {
            var request = await _client.GetAsync("/artist");
            request.EnsureSuccessStatusCode();
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
        
        [Test]
        public async Task Get_AllArtistsPartial_ReturnsOk_Test() {
            var response = await _client.GetAsync("/artist/partial");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Get_OneArtistById_ReturnsOk_Test() {
            var request = await _client.GetAsync("/artist/1");
            request.EnsureSuccessStatusCode();
            Assert.That(request.Content, Is.Not.Null);
        }

        [Test]
        public async Task Get_OneArtistById_ReturnsNotFound_Test() {
            var request = await _client.GetAsync("/artist/100");
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Get_OneArtistBySlug_ReturnsOk_Test() {
            var response = await _client.GetAsync("/artist/slug-leonardo");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            if (response.IsSuccessStatusCode) {
                string response_body = await response.Content.ReadAsStringAsync();
                JObject json_body = JObject.Parse(response_body);
                string artist_name = json_body["name"]!.ToString();
                Assert.That(artist_name, Is.EqualTo("Leonardo"));
            }
        }

        [Test]
        public async Task Get_OneArtistBySlug_ReturnsNotFound_Test() {
            var response = await _client.GetAsync("/artist/slug-da-vinci");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }
    }
}
