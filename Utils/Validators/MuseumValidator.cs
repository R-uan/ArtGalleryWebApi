using ArtGallery.Models;
using FluentValidation;

namespace ArtGallery.Utils.Validators {
	public class MuseumValidator : AbstractValidator<Museum> {
		public MuseumValidator() {
			RuleFor(museum => museum.Slug).NotEmpty().WithMessage("Provide a slug for museum ('museum-slug-example')");
			RuleFor(museum => museum.Name).NotEmpty().WithMessage("Name is required.").MinimumLength(4).WithMessage("Short name init ?");
			RuleFor(museum => museum.Country).NotEmpty().WithMessage("Country is required.").MinimumLength(4).WithMessage("Short name init ?");
		}
	}
}
