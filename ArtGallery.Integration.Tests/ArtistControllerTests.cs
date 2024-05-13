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

        public ArtistControllerTests() {
            _factory = new ArtGalleryWebAppFactory();
        }

        [Test]
        public async Task Get_AllArtists_ReturnsOk_Test() {
            var application = new ArtGalleryWebAppFactory();
            var client = application.CreateClient();
            var request = await client.GetAsync("/artist");
            request.EnsureSuccessStatusCode();
            Assert.That(request.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task Get_OneArtist_ReturnsOk_Test() {
            var application = new ArtGalleryWebAppFactory();

        }
    }
}
