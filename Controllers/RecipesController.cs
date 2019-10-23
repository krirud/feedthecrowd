using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedTheCrowd.Dtos.Products;
using FeedTheCrowd.Dtos.Recipes;
using FeedTheCrowd.Models;
using FeedTheCrowd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FeedTheCrowd.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    public class RecipesController : Controller
    {
        private readonly IRecipeService _recipeService;

        public RecipesController(IRecipeService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var recipes = await _recipeService.GetAll();
            return Ok(recipes);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddRecipe([FromBody] NewRecipeDto newRecipeDto)
        {
            var createRecipe = await _recipeService.Create(newRecipeDto);

            return Ok(createRecipe);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Recipe>> GetRecipe(int id)
        {
            var recipe = await _recipeService.GetById(id);
            if (recipe == null)
            {
                return NotFound();
            }

            return Ok(recipe);
        }
        [Authorize]
        [HttpGet("event/{eventId}")]
        public async Task<ActionResult> GetRecipesByEvent(int eventId)
        {
            var recipes = await _recipeService.GetRecipesByEvent(eventId);
            if (recipes == null)
            {
                return NotFound();
            }

            return Ok(recipes);
        }
        [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<ActionResult> GetRecipesByUser(int userId)

        {
            var recipes = await _recipeService.GetRecipesByUser(userId);
            if (recipes == null)
            {
                return NotFound();
            }

            return Ok(recipes);
        }
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Recipe>> Delete(int id)
        {
            var success = await _recipeService.Delete(id);
            if (success == null)
                return BadRequest("Cannot delete recipe");
            else if (success != id.ToString())
                return BadRequest("Recipe is in event list");
            else
                return Ok("deletion is succesful");
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NewRecipeDto newRecipe)
        {
            await _recipeService.Update(id, newRecipe);
            return NoContent();
        }

    }
}