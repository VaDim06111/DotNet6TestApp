namespace TestApp.CQRS.Queries
{
    public class GetHeroesByLastNameQuery : IRequest<List<SuperHero>>
    {
        public string LastName { get; }

        public GetHeroesByLastNameQuery(string lastName)
        {
            LastName = lastName;
        }

        public class GetHeroesByLastNameQueryHandler : IRequestHandler<GetHeroesByLastNameQuery, List<SuperHero>>
        {
            private readonly DataContext _context;

            public GetHeroesByLastNameQueryHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<SuperHero>> Handle(GetHeroesByLastNameQuery request, CancellationToken cancellationToken)
            {
                Log.Information($"Get heroes by lastName with value: {request.LastName} from database");
                return await _context.SuperHeroes.Where(h => h.LastName.Equals(request.LastName))
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
