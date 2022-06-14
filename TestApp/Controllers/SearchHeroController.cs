using Microsoft.AspNetCore.Mvc;
using TestApp.Core;

namespace TestApp.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchHeroController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchHeroController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        /// <returns>All heroes that match search parameters</returns>
        /// <response code="200">Returns all heroes that match search parameters</response>
        /// <response code="400">If ModelState is invalid</response>
        [HttpGet("{key}/{value}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(400, Type = typeof(ValidationProblemDetails))]
        public async Task<ActionResult<List<SuperHero>>> Get(string key, string value)
        {
            var model = new SearchModel()
            {
                Key = key,
                Value = value
            };

            var heroes = await _searchService.Search(model);

            Log.Information($"Get heroes by parameter: {model.Key} with value: {model.Value} action was succeeded");
            return Ok(heroes);
        }
    }
}
