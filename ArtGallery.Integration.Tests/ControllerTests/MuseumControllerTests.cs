using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ArtGallery.Models;

namespace ArtGallery.Integration.Tests.ControllerTests {
    public class MuseumControllerTests {

        private readonly ArtGalleryWebAppFactory _factory;
        private readonly HttpClient _client;

        public MuseumControllerTests() {
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
        public async Task Get_AllMuseums_ReturnsOk_Test() {
            var request = await _client.GetAsync("/museum");
            request.EnsureSuccessStatusCode();
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Get_AllMuseumsPartial_ReturnsOk_Test() {
            var response = await _client.GetAsync("/museum/partial");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Get_OneMuseumById_ReturnsOk_Test() {
            var request = await _client.GetAsync("/museum/1");
            request.EnsureSuccessStatusCode();
            Assert.That(request.Content, Is.Not.Null);
        }

        [Test]
        public async Task Get_OneMuseumById_ReturnsNotFound_Test() {
            var request = await _client.GetAsync("/museum/100");
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Get_OneMuseumBySlug_ReturnsOk_Test() {
            var response = await _client.GetAsync("/museum/museum-louvre");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            if (response.IsSuccessStatusCode) {
                string response_body = await response.Content.ReadAsStringAsync();
                JObject json_body = JObject.Parse(response_body);
                string museum_name = json_body["name"]!.ToString();
                Assert.That(museum_name, Is.EqualTo("Louvre"));
            }
        }

        [Test]
        public async Task Get_OneMuseumBySlug_ReturnsNotFound_Test() {
            var response = await _client.GetAsync("/museum/museum-beldona");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Post_OneMuseumWithOnlyRequired_ReturnsOk_Test() {
            var museum = new { Name = "Museu", Slug = "museum-museu", Country = "Brazil" };
            var request_body = JsonContent.Create(museum);
            var request = await _client.PostAsync("/museum", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            string response_body = await request.Content.ReadAsStringAsync();
            JObject parsed_response = JObject.Parse(response_body);
            Assert.That(parsed_response["name"]!.ToString(), Is.EqualTo("Museu"));
        }

        [Test]
        public async Task Post_OneMuseumWithAllFields_ReturnsOk_Test() {
            Museum museum = new() {
                Name = "Museu de Arte Moderna",
                Slug = "museu-arte-moderna",
                Country = "Brazil",
                State = "Bahia",
                City = "Salvador",
                Inauguration = 2010,
                Latitude = 0,
                Longitude = 0,
            };
            var request_body = JsonContent.Create(museum);
            var request = await _client.PostAsync("/museum", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            string response_body = await request.Content.ReadAsStringAsync();
            JObject parsed_response = JObject.Parse(response_body);
            Assert.That(parsed_response["name"]!.ToString, Is.EqualTo("Museu de Arte Moderna"));
        }

        [Test]
        public async Task Post_OneMuseumWithoutRequiredFields_ReturnsBadRequest_Test() {
            var museum = new { Country = "Italy" };
            var request_body = JsonContent.Create(museum);
            var request = await _client.PostAsync("/museum", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Patch_OneMuseum_ReturnsOk_Test() {
            var museum = new { Name = "Update Test" };
            var request_body = JsonContent.Create(museum);
            var request = await _client.PatchAsync("/museum/1", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var response_body = await request.Content.ReadAsStringAsync();
            JObject parsed_body = JObject.Parse(response_body);
            Assert.That(parsed_body["name"]!.ToString(), Is.EqualTo("Update Test"));
        }

        [Test]
        public async Task Patch_OneMuseum_ReturnsNotFound_Test() {
            var museum = new { Name = "Update Test" };
            var request_body = JsonContent.Create(museum);
            var request = await _client.PatchAsync("/museum/100", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_OneMuseumWithId_ReturnsOk_Test() {
            var request = await _client.DeleteAsync("/museum/3");
            string response_body = await request.Content.ReadAsStringAsync();
            Assert.Multiple(() => {
                Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response_body, Is.EqualTo("true"));
            });
        }
    }


}
