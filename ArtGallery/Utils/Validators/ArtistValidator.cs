using FluentValidation;
using ArtGallery.Models;
using ArtGallery.DTO;
namespace ArtGallery.Utils.Validators;

public class ArtistValidator : AbstractValidator<ArtistDTO> {
	public ArtistValidator() {
		RuleFor(artist => artist.Slug).NotEmpty().WithMessage("Provide a slug for artist ('artist-slug-example')");
		RuleFor(artist => artist.Name).NotEmpty().WithMessage("Name is required.").MinimumLength(4).WithMessage("Short name init ?");
	}
}
