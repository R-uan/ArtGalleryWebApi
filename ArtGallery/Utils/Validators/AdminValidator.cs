using FluentValidation;
using ArtGallery.Models;

namespace ArtGallery.Utils.Validators;
public class AdminValidator : AbstractValidator<Admin> {
    public AdminValidator() {
        RuleFor(artist => artist.Username).NotEmpty().WithMessage("Provide username.");
        RuleFor(artist => artist.Password).NotEmpty().WithMessage("Password is required.").MinimumLength(6).WithMessage("Short password init ?");
    }
}
