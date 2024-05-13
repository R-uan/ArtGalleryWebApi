using ArtGallery.Models;
using FluentValidation;

namespace ArtGallery.Utils.Validators {
	public class ArtworkValidator : AbstractValidator<Artwork> {
		public ArtworkValidator() {
			RuleFor(artwork => artwork.Slug).NotEmpty().WithMessage("Provide a slug for artwork ('artwork-slug-example')");
			RuleFor(artwork => artwork.Title).NotEmpty().WithMessage("Name is required.");
			RuleFor(artwork => artwork.ArtistId).NotEmpty().WithMessage("Provide Artist ID");
			RuleFor(artwork => artwork.ImageURL).NotEmpty().WithMessage("Provide a image url");
			RuleFor(artwork => artwork.Description).NotEmpty().WithMessage("Provide a description about the artwork.");
		}
	}
}
