using ColourMemory.Api.Models;

namespace ColourMemory.Api.Services.Interfaces
{
    public interface IGameService
    {
        Task<Game> CreateGameAsync();
        Task<Game?> GetGameAsync(string id);
        Task UpdateGameAsync(string id, Game game);
    }
}