namespace TestApp.CQRS.Commands.CommandEntities
{
    public class DeleteHeroCommand : IRequest<bool>
    {
        public int Id { get; }

        public DeleteHeroCommand(int id)
        {
            Id = id;
        }
    }
}
