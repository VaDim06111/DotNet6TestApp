namespace TestApp.Validators
{
    public class SearchModelValidator : AbstractValidator<SearchModel>
    {
        private readonly List<string> _conditions  = new () { "id", "name", "firstName", "lastName", "place"};

        public SearchModelValidator()
        {
            RuleFor(s => s.Key).Must(s => _conditions.Contains(s))
                .WithMessage("Please only use: " + string.Join(",", _conditions));
            RuleFor(s => s.Value).NotEmpty();
        }
    }
}
