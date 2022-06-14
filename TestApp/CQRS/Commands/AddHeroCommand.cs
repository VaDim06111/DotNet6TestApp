namespace TestApp.CQRS.Commands
{
    public class AddHeroCommand : IRequest<List<SuperHero>>
    {
        public SuperHero Hero { get; }

        public AddHeroCommand(SuperHero hero)
        {
            Hero = hero;
        }

        public class AddHeroHandler : IRequestHandler<AddHeroCommand, List<SuperHero>>
        {
            private readonly DataContext _context;

            public AddHeroHandler(DataContext context)
            {
                _context = context;
            }

            public async Task<List<SuperHero>> Handle(AddHeroCommand request, CancellationToken cancellationToken)
            {             
                await _context.SuperHeroes.AddAsync(request.Hero, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                Log.Information("Add new SuperHero to database");

                return await _context.SuperHeroes.ToListAsync(cancellationToken);
            }
        }
    }
}
