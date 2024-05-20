using ArtGallery.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace ArtGallery.Integration.Tests.ControllerTests {
    public class AdminControllerTests {
        private ArtGalleryWebAppFactory _factory;
        private HttpClient _client;

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
            var admin = new { username = "ruan", password = "12345678"};
            var request_body = JsonContent.Create(admin);
            var request = await _client.PostAsync("/auth", request_body);
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var response_body = await request.Content.ReadAsStringAsync();
            Assert.That(response_body, Is.EqualTo("1"));
        }
    }
}
