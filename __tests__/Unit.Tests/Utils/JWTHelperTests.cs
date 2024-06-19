using ArtGallery.Utils;
using ArtGallery.Models;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Unit.Tests.Utils
{
	public class JWTHelperTests
	{
		private IOptions<JWTSettings> _options { get; set; }

		[SetUp]
		public void Setup()
		{
			IConfiguration configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Test.json").Build();
			var settings = configuration.GetSection("Jwt").Get<JWTSettings>();
			this._options = Options.Create(settings!);
		}

		[Test]
		public void SuccessfulyGenerateTokenTest()
		{
			JWTHelper target = new JWTHelper(this._options);
			Admin admin = new Admin() { Username = "La creatura", Password = "this-should-be-hashed" };
			//
			// The generate method only receives a valid Admin entity.
			string generate = target.Generate(admin);
			Assert.That(generate, Is.Not.Null);
		}
	}
}