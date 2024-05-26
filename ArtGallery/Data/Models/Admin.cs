using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ArtGallery.Models;

public class Admin { 
	public int AdminId { get; set; }
	[Required] public required string Username { get; set; }
	[Required] public required string Password { get; set; }
}