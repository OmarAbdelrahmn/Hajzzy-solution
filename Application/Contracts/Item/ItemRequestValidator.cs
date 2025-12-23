using FluentValidation;

namespace Application.Contracts.Item;

public class ItemRequestValidator : AbstractValidator<ItemRequest>
{
    public ItemRequestValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty();

        RuleFor(c => c.Price)
            .NotEmpty();

        RuleFor(c => c.Count)
            .NotEmpty();

        RuleFor(c => c.EffectiveSubstance)
            .NotEmpty();

        RuleFor(c => c.Brand)
            .NotEmpty();

        RuleFor(c => c.Type)
            .NotEmpty();
    }
}
