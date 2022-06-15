using AutoMapper;

namespace TestApp.CQRS.Commands
{
    public class UpdateHeroCommand : IRequest<bool>
    {
        public SuperHero Hero { get; }

        public UpdateHeroCommand(SuperHero hero)
        {
            Hero = hero;           
        }

        public class UpdateHeroHandler : IRequestHandler<UpdateHeroCommand, bool>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public UpdateHeroHandler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<bool> Handle(UpdateHeroCommand request, CancellationToken cancellationToken)
            {
                var dbHero = await _context.SuperHeroes.FindAsync(new object[] { request.Hero.Id }, cancellationToken);

                if (dbHero != null)
                {
                    _mapper.Map(request.Hero, dbHero);                  
                    await _context.SaveChangesAsync(cancellationToken);

                    Log.Information($"Update SuperHero with id: {request.Hero.Id} in database");
                    return true;
                }

                Log.Warning($"SuperHero with id: {request.Hero.Id} not found");
                return false;
            }
        }       
    }
}
