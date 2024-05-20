using ArtGallery.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ArtGallery.Integration.Tests.ControllerTests {
    [TestFixture]
    public class AdminControllerTests {
        private readonly ArtGalleryWebAppFactory _factory;
        private readonly HttpClient _client;

        public AdminControllerTests() {
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
        public async Task Post_Register_ReturnsOk_Test() {
            var admin = new { username = "ruan", password = "12345678" };
            var request_body = JsonContent.Create(admin);
            var request = await _client.PostAsync("/auth", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var response_body = await request.Content.ReadAsStringAsync();
            Assert.That(response_body, Is.EqualTo("2"));
        }
        
        [Test]
        public async Task Post_Register_ReturnsBadRequest_Test() {
            var admin = new { username = "ruan" };
            var request_body = JsonContent.Create(admin);
            var request = await _client.PostAsync("/auth", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task Get_Authenticate_ReturnsOk_Test() {
            var username = "admin";
            var password = "12345678";
            var authToken = Encoding.ASCII.GetBytes($"{username}:{password}");
            using (var client = _factory.CreateClient()) {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
                var request = await client.GetAsync("/auth");
                Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            }
        }

        [Test]
        public async Task Get_Authenticate_ReturnsUnauthorized_Test() {
            var username = "admin";
            var password = "1234567";
            var authToken = Encoding.ASCII.GetBytes($"{username}:{password}");
            using (var client = _factory.CreateClient()) {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authToken));
                var request = await client.GetAsync("/auth");
                Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.Unauthorized));
            }
        }
    }
}
