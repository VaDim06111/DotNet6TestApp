namespace TestApp.CQRS.Queries.QueryEntities
{
    public class FindHeroByIdQuery : IRequest<SuperHero>
    {
        public int Id { get; }

        public FindHeroByIdQuery(int id)
        {
            Id = id;
        }
    }
}
