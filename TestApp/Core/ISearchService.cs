namespace TestApp.Core
{
    public interface ISearchService
    {
        Task<List<SuperHero>> Search(SearchModel model);
    }
}
