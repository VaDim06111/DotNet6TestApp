namespace TestApp.CQRS.Commands.Handlers
{
    public class DeleteHeroHandler : IRequestHandler<DeleteHeroCommand, bool>
    {
        private readonly DataContext _context;

        public DeleteHeroHandler(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteHeroCommand request, CancellationToken cancellationToken)
        {
            var dbHero = await _context.SuperHeroes.FindAsync(new object[] { request.Id }, cancellationToken);

            if (dbHero != null)
            {
                _context.SuperHeroes.Remove(dbHero);
                await _context.SaveChangesAsync(cancellationToken);

                Log.Information($"Delete SuperHero with id: {request.Id} from database");

                return true;
            }

            Log.Warning($"Hero with id: {request.Id} not found");
            return false;                
        }
    }
}
