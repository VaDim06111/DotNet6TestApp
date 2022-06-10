namespace TestApp.CQRS.Commands.Handlers
{
    public class UpdateHeroHandler : IRequestHandler<UpdateHeroCommand, bool>
    {
        private readonly DataContext _context;

        public UpdateHeroHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(UpdateHeroCommand request, CancellationToken cancellationToken)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(new object[] { request.Hero.Id }, cancellationToken);

            if (dbHero != null)
            {
                dbHero.FirstName = request.Hero.FirstName;
                dbHero.LastName = request.Hero.LastName;
                dbHero.Place = request.Hero.Place;

                await _context.SaveChangesAsync(cancellationToken);

                Log.Information($"Update SuperHero with id: {request.Hero.Id} in database");

                return true;
            }

            Log.Warning($"SuperHero with id: {request.Hero.Id} not found");
            return false;
        }
    }
}
