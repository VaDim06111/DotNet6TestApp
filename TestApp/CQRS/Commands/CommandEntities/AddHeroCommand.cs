namespace TestApp.CQRS.Commands.CommandEntities
{
    public class AddHeroCommand : IRequest<List<SuperHero>>
    {
        public SuperHero Hero { get; }

        public AddHeroCommand(SuperHero hero)
        {
            Hero = hero;
        }
    }
}
