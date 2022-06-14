namespace TestApp.Validators
{
    public class AddHeroModelValidation : AbstractValidator<AddHeroModel>
    {
        public AddHeroModelValidation()
        {
            RuleFor(x => x).NotEmpty();
            RuleFor(x => x.Id).Equal(0);
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.Place).NotEmpty();
        }
    }
}
