namespace ArtGallery.Utils;

public class JWTSettings
{
	public required string Issuer { get; set; }
	public required string SecretKey { get; set; }
}
