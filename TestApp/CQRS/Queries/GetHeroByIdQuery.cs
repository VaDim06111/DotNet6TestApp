namespace TestApp.CQRS.Queries
{
    public class GetHeroByIdQuery : IRequest<SuperHero>
    {
        public int Id { get; }

        public GetHeroByIdQuery(int id)
        {
            Id = id;
        }

        public class FindHeroByIdHandler : IRequestHandler<GetHeroByIdQuery, SuperHero>
        {
            private readonly DataContext _context;

            public FindHeroByIdHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<SuperHero> Handle(GetHeroByIdQuery request, CancellationToken cancellationToken)
            {
                Log.Information($"Get SuperHero with id: {request.Id} from database");
                return await _context.SuperHeroes
                    .FindAsync(new object[] { request.Id }, cancellationToken);
            }
        }
    }
}
