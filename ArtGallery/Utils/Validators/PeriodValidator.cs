using ArtGallery.Models;
using FluentValidation;

namespace ArtGallery;

public class PeriodValidator : AbstractValidator<PeriodDTO> {
	public PeriodValidator() {
		RuleFor(period => period.Name).NotEmpty().WithMessage("Period name is required.");
		RuleFor(period => period.Summary).NotEmpty().WithMessage("Provide a summary about the period.");
	}
}
