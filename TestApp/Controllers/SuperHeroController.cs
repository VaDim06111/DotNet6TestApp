using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApp.Models;

namespace TestApp.Controllers
{
    [Route("api/hero")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {     
        private readonly DataContext _dbContext;

        public SuperHeroController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <returns>All heroes</returns>
        /// <response code="200">Returns all heroes</response>
        [HttpGet]    
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _dbContext.SuperHeroes.ToListAsync());
        }

        /// <returns>A SuperHero find by id</returns>
        /// <response code="200">Returns the SuperHero found by id</response>
        /// <response code="400">If hero not found</response>
        [HttpGet("{id}")]       
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _dbContext.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");
            return Ok(hero);
        }

        /// <returns>Heroes with newly created</returns>
        /// <response code="201">Returns heroes with newly created</response>      
        [HttpPost]      
        public async Task<ActionResult<List<SuperHero>>> Post(SuperHero hero)
        {
            await _dbContext.SuperHeroes.AddAsync(hero);
            await _dbContext.SaveChangesAsync();

            return new ObjectResult(await _dbContext.SuperHeroes.ToListAsync()) { StatusCode = StatusCodes.Status201Created }; ;           
        }

        /// <returns>Heroes with updated hero</returns>
        /// <response code="200">Returns heroes with updated hero</response>
        /// /// <response code="400">If hero not found</response>
        [HttpPut]       
        public async Task<ActionResult<List<SuperHero>>> Put(SuperHero request)
        {
            var dbHero = await _dbContext.SuperHeroes.FindAsync(request.Id);
            if (dbHero == null)
                return BadRequest("Hero not found");

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.SuperHeroes.ToListAsync());
        }
       
        /// <returns>All heroes without deleted</returns>
        /// <response code="200">Returns the heroes without deleted</response>
        /// <response code="400">If hero not found</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var hero = await _dbContext.SuperHeroes.FindAsync(id);
            if (hero == null)
                return BadRequest("Hero not found");

            _dbContext.SuperHeroes.Remove(hero);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.SuperHeroes.ToListAsync());
        }
    }
}
