namespace TestApp.CQRS.Commands.CommandEntities
{
    public class UpdateHeroCommand : IRequest<bool>
    {
        public SuperHero Hero { get; }

        public UpdateHeroCommand(SuperHero hero)
        {
            Hero = hero;
        }
    }
}
