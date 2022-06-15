using Microsoft.AspNetCore.Mvc;
using TestApp.Core;

namespace TestApp.Controllers
{
    [Route("api/search")]
    [ApiController]
    public class SearchHeroController : ControllerBase
    {
        private readonly ISearchService _searchService;
        private readonly IValidator<SearchModel> _validator;

        public SearchHeroController(ISearchService searchService, IValidator<SearchModel> validator)
        {
            _searchService = searchService;
            _validator = validator;
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
            
            var validationResult = _validator.Validate(model);
            

            if (validationResult.IsValid)
            {
                var heroes = await _searchService.Search(model);

                Log.Information($"Get heroes by parameter: {model.Key} with value: {model.Value} action was succeeded");
                return Ok(heroes);
            }

            return BadRequest($"ModelState is invalid. Errors: {string.Join($",", validationResult.Errors.Select(s => s.ErrorMessage))}");
        }
    }
}
