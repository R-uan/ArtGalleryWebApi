using ArtGallery.Models;
using ArtGallery.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGallery.Unit.Tests.Utils { 
    [TestFixture]
    public class AuthHelperTest {
        [Test]
        public void GenerateJwtToken() {
            Admin admin = new Admin() { Username = "Username", Password = "Password" };
            var token = AuthHelper.GenerateJWTToken(admin);
            Assert.That(token, Is.Not.Null);
        }
    }
}
