using Backend_Final.Areas.Admin.ViewModels.ProductImage;
using Backend_Final.Contracts.ProductImage;
using Backend_Final.Validators;
using FluentValidation;

namespace Backend_Final.Areas.Admin.Validators.Admin.ProductImage
{
    public class AddViewModelValidator : AbstractValidator<AddViewModel>
    {
        public AddViewModelValidator()
        {
            RuleFor(avm => avm.Image)
               .Cascade(CascadeMode.Stop)

               .NotNull()
               .WithMessage("Image can't be empty")

               .SetValidator(
                    new FileValidator(2, FileSizes.Mega,
                        FileExtensions.JPG.GetExtension(), FileExtensions.PNG.GetExtension())!);
        }
    }
}
