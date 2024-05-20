using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ArtGallery.Models;
using ArtGallery.Utils;

namespace ArtGallery.Integration.Tests.ControllerTests {
    [TestFixture]
    public class ArtworkControllerTests {
        private readonly ArtGalleryWebAppFactory _factory;
        private readonly HttpClient _client;

        public ArtworkControllerTests() {
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
        public async Task Get_AllArtworks_ReturnsOk_Test() {
            var request = await _client.GetAsync("/artwork");
            request.EnsureSuccessStatusCode();
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Get_AllArtworksPartial_ReturnsOk_Test() {
            var response = await _client.GetAsync("/artwork/partial");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Get_OneArtworkById_ReturnsOk_Test() {
            var request = await _client.GetAsync("/artwork/1");
            request.EnsureSuccessStatusCode();
            Assert.That(request.Content, Is.Not.Null);
        }

        [Test]
        public async Task Get_OneArtworkById_ReturnsNotFound_Test() {
            var request = await _client.GetAsync("/artwork/100");
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Get_OneArtworkBySlug_ReturnsOk_Test() {
            var response = await _client.GetAsync("/artwork/alisa-mona");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            if (response.IsSuccessStatusCode) {
                string response_body = await response.Content.ReadAsStringAsync();
                JObject json_body = JObject.Parse(response_body);
                string artwork_name = json_body["title"]!.ToString();
                Assert.That(artwork_name, Is.EqualTo("Alisa"));
            }
        }

        [Test]
        public async Task Get_OneArtworkBySlug_ReturnsNotFound_Test() {
            var response = await _client.GetAsync("/artwork/mona-lisa");
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Get_AllPartialPagination_ReturnsOk_Test() {
            var request = await _client.GetAsync("/artwork/partial/paginate?page_index=1&page_size=1");
            JObject response = JObject.Parse(await request.Content.ReadAsStringAsync());
            Assert.Multiple(() => {
                Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response["hasNextPage"]!.ToString(), Is.EqualTo("True"));
            });
        }

        [Test]
        public async Task Get_AllPartialPagination2_ReturnsOk_Test() {
            var request = await _client.GetAsync("/artwork/partial/paginate?page_index=2&page_size=1");
            JObject response = JObject.Parse(await request.Content.ReadAsStringAsync());
            Assert.Multiple(() => {
                Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response["hasPreviousPage"]!.ToString(), Is.EqualTo("True"));
            });
        }


        [Test]
        public async Task Post_OneArtworkWithOnlyRequired_ReturnsOk_Test() {
            Artwork artwork = new() { Title = "banana", ImageURL = "image of a banana", History = "its yellow", Period = "0", Slug = "ba-nana", ArtistId = 1, MuseumId = 1 };
            var request_body = JsonContent.Create(artwork);
            var request = await _client.PostAsync("/artwork", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            string response_body = await request.Content.ReadAsStringAsync();
            JObject parsed_response = JObject.Parse(response_body);
            Assert.That(parsed_response["title"]!.ToString(), Is.EqualTo("banana"));
        }

        [Test]
        public async Task Post_OneArtworkWithAllFields_ReturnsOk_Test() {
            Artwork artwork = new() { Title = "a", Slug = "a", History = "a", ImageURL = "a", Period = "a", MuseumId = 1, ArtistId = 1, Year = 0 };
            var request_body = JsonContent.Create(artwork);
            var request = await _client.PostAsync("/artwork", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            string response_body = await request.Content.ReadAsStringAsync();
            JObject parsed_response = JObject.Parse(response_body);
            Assert.That(parsed_response["title"]!.ToString, Is.EqualTo("a"));
        }

        [Test]
        public async Task Post_OneArtworkWithoutRequiredFields_ReturnsBadRequest_Test() {
            var artwork = new { Period = "Today" };
            var request_body = JsonContent.Create(artwork);
            var request = await _client.PostAsync("/artwork", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Patch_OneArtwork_ReturnsOk_Test() {
            var artwork = new { Title = "Update Test" };
            var request_body = JsonContent.Create(artwork);
            var request = await _client.PatchAsync("/artwork/1", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var response_body = await request.Content.ReadAsStringAsync();
            JObject parsed_body = JObject.Parse(response_body);
            Assert.That(parsed_body["title"]!.ToString(), Is.EqualTo("Update Test"));
        }

        [Test]
        public async Task Patch_OneArtwork_ReturnsNotFound_Test() {
            var artwork = new { Name = "Update Test" };
            var request_body = JsonContent.Create(artwork);
            var request = await _client.PatchAsync("/artwork/100", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
        }

        [Test]
        public async Task Delete_OneArtistWithId_ReturnsOk_Test() {
            var request = await _client.DeleteAsync("/artwork/3");
            string response_body = await request.Content.ReadAsStringAsync();
            Assert.Multiple(() => {
                Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
                Assert.That(response_body, Is.EqualTo("true"));
            });
        }
    }
}
