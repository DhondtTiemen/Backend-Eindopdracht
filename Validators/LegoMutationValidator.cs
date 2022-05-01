namespace Eindopdracht.Validators;

public class SetValidatorMutation : AbstractValidator<AddSetInput>
{
    public SetValidatorMutation()
    {
        RuleFor(s => s.number).NotEmpty().WithMessage("Setnumber is required for a set!!!");
        RuleFor(s => s.name).NotEmpty().WithMessage("Name is required for a set!!!");
        RuleFor(s => s.Theme).NotEmpty().WithMessage("Theme is required for a set!!!");
    }
}

public class ThemeValidatorMutation : AbstractValidator<AddThemeInput>
{
    public ThemeValidatorMutation()
    {
        RuleFor(t => t.name).NotEmpty().WithMessage("Name is required for a theme!!!");
    }
}

public class CustomerValidatorMutation : AbstractValidator<AddCustomerInput>
{
    public CustomerValidatorMutation()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required for a customer!!!");
        RuleFor(c => c.Email).NotEmpty().WithMessage("Email is required for a customer!!!");
    }
}

public class OrderValidatorMutation : AbstractValidator<AddOrderInput>
{
    public OrderValidatorMutation()
    {
        RuleFor(o => o.customer).NotEmpty().WithMessage("Customer is required for an order!!!");
        RuleFor(o => o.set).NotEmpty().WithMessage("Set is required for an order!!!");
    }
}