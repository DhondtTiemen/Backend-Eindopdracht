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