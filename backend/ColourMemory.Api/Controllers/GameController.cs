using ColourMemory.Api.Models;
using ColourMemory.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ColourMemory.Api.Controllers
{
    [ApiController]
    [Route("api/game")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("start")]
        public async Task<ActionResult<Game>> StartGame()
        {
            var game = await _gameService.CreateGameAsync();
            return Ok(game);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(string id)
        {
            var game = await _gameService.GetGameAsync(id);
            if (game == null) return NotFound();
            return Ok(game);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGame(string id, Game updatedGame)
        {
            await _gameService.UpdateGameAsync(id, updatedGame);
            return NoContent();
        }
    }
}
