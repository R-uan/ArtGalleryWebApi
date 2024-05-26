namespace ArtGallery.DTO {
	public struct PartialMuseumDTO(int id, string name, string country) {
		public int MuseumId { get; set; } = id;
		public string Name { get; set; } = name;
		public string Country { get; set; } = country;
	}
}