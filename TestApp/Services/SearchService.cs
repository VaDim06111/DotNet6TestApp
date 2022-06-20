using TestApp.Core;

namespace TestApp.Services
{
    public class SearchService : ISearchService
    {
        private readonly IMediator _mediator;

        public SearchService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<List<SuperHero>> Search(SearchModel model)
        {
            var heroes = new List<SuperHero>();
      
            switch (model.Key)
            {
                case "id":
                    var hero = await _mediator.Send(new GetHeroByIdQuery(int.Parse(model.Value)));
                    if (hero is not null)
                        heroes.Add(hero);
                    break;
                case "name":
                    heroes = await _mediator.Send(new GetHeroesByNameQuery(model.Value));
                    break;
                case "firstName":
                    heroes = await _mediator.Send(new GetHeroesByFirstNameQuery(model.Value));
                    break;
                case "lastName":
                    heroes = await _mediator.Send(new GetHeroesByLastNameQuery(model.Value));
                    break;
                case "place":
                    heroes = await _mediator.Send(new GetHeroesByPlaceQuery(model.Value));
                    break;
                default:
                    break;
            }

            return heroes;
        }
    }
}
