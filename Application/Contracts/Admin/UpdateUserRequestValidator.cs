using Application.Contracts.Admin;
using FluentValidation;

namespace Medical_E_Commerce.Contracts.Admin;

public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.UserFullName)
            .NotEmpty()
            .WithMessage("UserFullName is required")
            .Length(3, 100);


        RuleFor(x => x.UserAddress)
            .NotEmpty()
            .WithMessage("User address is required")
            .Length(3, 100);

        RuleFor(x => x.Roles)
            .NotEmpty()
            .NotNull();

        RuleFor(i => i.Roles)
            .Must(i => i.Distinct().Count() == i.Count)
            .WithMessage("you can't add duplicated permission for the role")
            .When(c => c.Roles != null);
    }
}
