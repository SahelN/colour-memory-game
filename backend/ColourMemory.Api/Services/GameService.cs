using ColourMemory.Api.Models;
using ColourMemory.Api.Services.Interfaces;
using ColourMemory.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace ColourMemory.Api.Services
{
    public class GameService : IGameService
    {
        private readonly IMongoCollection<Game> _games;
        private readonly ILogger<GameService> _logger;

        private const string CollectionName = "Games";

        public GameService(IOptions<MongoDbSettings> settings, ILogger<GameService> logger)
        {
            _logger = logger;

            try
            {
                var client = new MongoClient(settings.Value.ConnectionString);
                var database = client.GetDatabase(settings.Value.DatabaseName);
                _games = database.GetCollection<Game>(CollectionName);

                _logger.LogInformation("Successfully connected to MongoDB database: {DatabaseName}", settings.Value.DatabaseName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to connect to MongoDB. Please check the connection string or database settings.");
                throw; // Important: Re-throw the exception to fail fast if critical service can't start
            }
        }

        public async Task<Game> CreateGameAsync()
        {
            try
            {
                _logger.LogInformation("Starting CreateGameAsync method...");
                var game = new Game
                {
                    Cards = GenerateShuffledCards(),
                    Score = 0,
                    IsGameOver = false
                };
                await _games.InsertOneAsync(game);
                _logger.LogInformation("New game created successfully with {CardCount} cards.", game.Cards.Count);

                return game;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the game.");
                throw new ApplicationException("An error occurred while creating the game.", ex);
            }
        }

        private List<Card> GenerateShuffledCards()
        {
            var colors = new[] { "red", "blue", "green", "yellow", "purple", "orange", "pink", "brown" };
            var duplicatedColors = colors.Concat(colors).OrderBy(x => Guid.NewGuid()).ToArray();
            return duplicatedColors.Select(color => new Card { Color = color }).ToList();
        }

        public async Task<Game?> GetGameAsync(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogWarning("GetGameAsync called with empty or null id.");
                throw new ArgumentException("Id must be provided.");
            }

            var game = await _games.Find(g => g.Id == id).FirstOrDefaultAsync();

            if (game == null)
            {
                _logger.LogWarning("No game found with ID: {GameId}", id);
            }
            else
            {
                _logger.LogInformation("Game retrieved successfully with ID: {GameId}", id);
            }

            return game;
        }

        public async Task UpdateGameAsync(string id, Game game)
        {
            if (string.IsNullOrWhiteSpace(id) || game == null)
            {
                _logger.LogWarning("Invalid input when updating game: id is '{GameId}', game object is null: {IsGameNull}", id, game == null);
                throw new ArgumentException("Invalid game data or ID.");
            }

            var result = await _games.ReplaceOneAsync(g => g.Id == id, game);

            if (result.IsAcknowledged && result.ModifiedCount > 0)
            {
                _logger.LogInformation("Successfully updated game with ID: {GameId}", id);
            }
            else
            {
                _logger.LogWarning("No game was updated. Game with ID {GameId} may not exist.", id);
            }
        }
    }
}
