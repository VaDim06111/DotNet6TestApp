﻿using Microsoft.AspNetCore.Mvc;

namespace TestApp.Controllers
{
    [Route("api/hero")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SuperHeroController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <returns>All heroes</returns>
        /// <response code="200">Returns all heroes</response>
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {         
            return Ok(await _mediator.Send(new GetAllHeroesQuery()));
        }

        /// <returns>A SuperHero find by id</returns>
        /// <response code="200">Returns the SuperHero found by id</response>
        /// <response code="400">If hero not found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> Get(int id)
        {
            var hero = await _mediator.Send(new FindHeroByIdQuery(id));
            if (hero == null)
            {
                Log.Warning($"Get hero by id: {id} action was failed. Hero not found");
                return BadRequest("Hero not found");
            }


            Log.Information($"Get hero by id: {id} action was succeeded");
            return Ok(hero);
        }

        /// <returns>Heroes with newly created</returns>
        /// <response code="201">Returns heroes with newly created</response>      
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> Post(SuperHero hero)
        {
            var heroes = await _mediator.Send(new AddHeroCommand(hero));

            Log.Information($"Add hero by id: {hero.Id} action was succeeded");
            return new ObjectResult(heroes) { StatusCode = StatusCodes.Status201Created };
        }

        /// <returns>Heroes with updated hero</returns>
        /// <response code="200">Returns heroes with updated hero</response>
        /// /// <response code="400">If hero not found</response>
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> Put(SuperHero request)
        {
            var result = await _mediator.Send(new UpdateHeroCommand(request));

            if (result)
            {
                Log.Information($"Update hero by id: {request.Id} action was succeeded");
                return Ok(await _mediator.Send(new GetAllHeroesQuery()));
            }

            Log.Warning($"Update hero by id: {request.Id} action was failed");
            return BadRequest("Hero not found");
        }

        /// <returns>All heroes without deleted</returns>
        /// <response code="200">Returns the heroes without deleted</response>
        /// <response code="400">If hero not found</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteHeroCommand(id));

            if (result)
            {
                Log.Information($"Delete hero by id: {id} action was succeeded");
                return Ok(await _mediator.Send(new GetAllHeroesQuery()));
            }


            Log.Warning($"Delete hero by id: {id} action was failed. Hero not found");
            return BadRequest("Hero not found");
        }
    }
}
