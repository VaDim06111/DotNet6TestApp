namespace TestApp.CQRS.Queries
{
    public class GetHeroesByNameQuery : IRequest<List<SuperHero>>
    {
        public string Name { get; }

        public GetHeroesByNameQuery(string name)
        {
            Name = name;
        }

        public class GetHeroesByNameQueryHandler : IRequestHandler<GetHeroesByNameQuery, List<SuperHero>>
        {
            private readonly DataContext _context;

            public GetHeroesByNameQueryHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<SuperHero>> Handle(GetHeroesByNameQuery request, CancellationToken cancellationToken)
            {
                Log.Information($"Get heroes by name with value: {request.Name} from database");
                return await _context.SuperHeroes.Where(h => h.Name.Equals(request.Name))
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
