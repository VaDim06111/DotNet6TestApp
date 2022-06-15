namespace TestApp.Validators
{
    public class SuperHeroValidator : AbstractValidator<SuperHero>
    {
        public SuperHeroValidator()
        {           
            RuleFor(x => x.Id).Equal(0)
                .WithMessage("Please only use default value: 0");
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Place).NotEmpty();
        }
    }
}
