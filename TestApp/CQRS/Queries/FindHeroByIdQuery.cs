namespace TestApp.CQRS.Queries
{
    public class FindHeroByIdQuery : IRequest<SuperHero>
    {
        public int Id { get; }

        public FindHeroByIdQuery(int id)
        {
            Id = id;
        }

        public class FindHeroByIdHandler : IRequestHandler<FindHeroByIdQuery, SuperHero>
        {
            private readonly DataContext _context;

            public FindHeroByIdHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<SuperHero> Handle(FindHeroByIdQuery request, CancellationToken cancellationToken)
            {
                Log.Information($"Get SuperHero with id: {request.Id} from database");
                return await _context.SuperHeroes
                    .FindAsync(new object[] { request.Id }, cancellationToken);
            }
        }
    }
}
