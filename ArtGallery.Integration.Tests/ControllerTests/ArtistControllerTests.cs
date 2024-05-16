using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Integration.Tests.ControllerTests {
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

        [Test]
        public async Task Post_OneArtistWithOnlyRequired_ReturnsOk_Test() {
            var artist = new { Name = "Leonardo Da Vinci", Slug = "da-vinki-question-mark" };
            var request_body = JsonContent.Create(artist);
            var request = await _client.PostAsync("/artist", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            string response_body = await request.Content.ReadAsStringAsync();
            JObject parsed_response = JObject.Parse(response_body);
            Assert.That(parsed_response["name"]!.ToString(), Is.EqualTo("Leonardo Da Vinci"));
        }

        [Test]
        public async Task Post_OneArtistWithAllFields_ReturnsOk_Test() {
            var artist = new {
                Name = "Leonardo Da Vinci",
                Slug = "da-vinki-question-mark",
                Country = "Italy",
                Movement = "Idk",
                Biography = "He did paintings and stuff",
                Profession = "Painter"
            };
            var request_body = JsonContent.Create(artist); 
            var request = await _client.PostAsync("/artist", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            string response_body = await request.Content.ReadAsStringAsync();
            JObject parsed_response = JObject.Parse(response_body);
            Assert.That(parsed_response["name"]!.ToString, Is.EqualTo("Leonardo Da Vinci"));
        }

        [Test]
        public async Task Post_OneArtistWithoutRequiredFields_ReturnsBadRequest_Test() {
            var artist = new { Country = "Italia" };
            var request_body = JsonContent.Create(artist);
            var request = await _client.PostAsync("/artist", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Patch_OneArtist_ReturnsOk_Test() {
            var artist = new { Name = "Update Test" };
            var request_body = JsonContent.Create(artist);
            var request = await _client.PatchAsync("/artist/1", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var response_body = await request.Content.ReadAsStringAsync();
            JObject parsed_body = JObject.Parse(response_body);
            Assert.That(parsed_body["name"]!.ToString(), Is.EqualTo("Update Test"));
        }

        [Test]
        public async Task Patch_OneArtist_ReturnsNotFound_Test() {
            var artist = new { Name = "Update Test" };
            var request_body = JsonContent.Create(artist);
            var request = await _client.PatchAsync("/artist/100", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_OneArtistWithId_ReturnsOk_Test() {
            var request = await _client.DeleteAsync("/artist/3");
            string response_body = await request.Content.ReadAsStringAsync();
            Assert.Multiple(() => {
                Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response_body, Is.EqualTo("true"));
            });
        }
    }
}
