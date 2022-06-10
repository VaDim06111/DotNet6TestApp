namespace TestApp.CQRS.Queries.Handlers
{
    public class GetAllHeroesHandler : IRequestHandler<GetAllHeroesQuery, List<SuperHero>>
    {
        private readonly DataContext _context;

        public GetAllHeroesHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<List<SuperHero>> Handle(GetAllHeroesQuery request, CancellationToken cancellationToken)
        {
            Log.Information($"Get all SuperHeroes from database");
            return await _context.SuperHeroes.ToListAsync(cancellationToken);
        }
    }
}
