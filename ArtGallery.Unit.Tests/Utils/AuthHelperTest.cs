using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ArtGallery.Unit.Tests;

[TestFixture]
public class AuthHelperTest {
	private readonly IConfiguration Configuration = new ConfigurationBuilder().AddJsonFile("appsettings.Unit.Test.json").Build();

	[SetUp]
	public void SetUp() {
		var Settings = new ServiceCollection().Configure<JWTSettings>(Configuration.GetSection("Jwt"));

	}
}
