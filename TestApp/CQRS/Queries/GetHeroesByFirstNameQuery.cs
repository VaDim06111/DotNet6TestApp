namespace TestApp.CQRS.Queries
{
    public class GetHeroesByFirstNameQuery : IRequest<List<SuperHero>>
    {
        public string FirstName { get; }

        public GetHeroesByFirstNameQuery(string firstName)
        {
            FirstName = firstName;
        }

        public class GetHeroesByFirstNameQueryHandler : IRequestHandler<GetHeroesByFirstNameQuery, List<SuperHero>>
        {
            private readonly DataContext _context;

            public GetHeroesByFirstNameQueryHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<SuperHero>> Handle(GetHeroesByFirstNameQuery request, CancellationToken cancellationToken)
            {
                Log.Information($"Get heroes by firstName with value: {request.FirstName} from database");
                return await _context.SuperHeroes.Where(h => h.FirstName.Equals(request.FirstName))
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
