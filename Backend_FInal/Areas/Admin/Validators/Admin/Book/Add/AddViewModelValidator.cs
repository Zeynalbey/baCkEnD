using Backend_Final.Areas.Admin.ViewModels.Product.Add;
using FluentValidation;

namespace Backend_Final.Areas.Admin.Validators.Admin.Product.Add
{
    public class AddViewModelValidator : AbstractValidator<AddViewModel>
    {
        public AddViewModelValidator()
        {
            RuleFor(avm => avm.Title)
                .NotNull()
                .WithMessage("Title can't be empty")
                .NotEmpty()
                .WithMessage("Title can't be empty")
                .MinimumLength(10)
                .WithMessage("Minimum length should be 10")
                .MaximumLength(45)
                .WithMessage("Maximum length should be 45");
        }
    }
}
