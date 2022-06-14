namespace TestApp.Validators
{
    public class UpdateHeroModelValidation : AbstractValidator<UpdateHeroModel>
    {
        public UpdateHeroModelValidation()
        {
            RuleFor(x => x).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.Place).NotEmpty();
        }
    }
}
