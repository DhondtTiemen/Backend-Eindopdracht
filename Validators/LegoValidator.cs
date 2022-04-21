namespace Eindopdracht.Validators;

public class SetValidator : AbstractValidator<Set>
{
    public SetValidator()
    {
        RuleFor(s => s.SetNumber).NotEmpty().WithMessage("Setnumber is required for a set!!!");
        RuleFor(s => s.Name).NotEmpty().WithMessage("Name is required for a set!!!");
        RuleFor(s => s.Theme).NotEmpty().WithMessage("Theme is required for a set!!!");
    }
}

public class ThemeValidator : AbstractValidator<Theme>
{
    public ThemeValidator()
    {
        RuleFor(t => t.Name).NotEmpty().WithMessage("Name is required for a theme!!!");
    }
}

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required for a customer!!!");
        RuleFor(c => c.Email).NotEmpty().WithMessage("Email is required for a customer!!!");
    }
}

public class OrderValidator : AbstractValidator<Order>
{
    public OrderValidator()
    {
        RuleFor(o => o.Customer).NotEmpty().WithMessage("Customer is required for an order!!!");
        RuleFor(o => o.Set).NotEmpty().WithMessage("Set is required for an order!!!");
    }
}