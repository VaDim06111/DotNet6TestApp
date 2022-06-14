namespace TestApp.CQRS.Queries
{
    public class GetHeroesByPlaceQuery : IRequest<List<SuperHero>>
    {
        public string Place { get; }

        public GetHeroesByPlaceQuery(string place)
        {
            Place = place;
        }

        public class GetHeroesByPlaceQueryHandler : IRequestHandler<GetHeroesByPlaceQuery, List<SuperHero>>
        {
            private readonly DataContext _context;

            public GetHeroesByPlaceQueryHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<SuperHero>> Handle(GetHeroesByPlaceQuery request, CancellationToken cancellationToken)
            {
                Log.Information($"Get heroes by place with value: {request.Place} from database");
                return await _context.SuperHeroes.Where(h => h.Place.Equals(request.Place))
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
