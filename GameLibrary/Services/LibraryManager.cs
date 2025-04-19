using GameLibrary.Entities;

namespace GameLibrary.Services;

public sealed class LibraryManager(Statics statics) : ILibraryManager
{
	public void AddGame(Game game)
	{
		using var dbContext = statics.CreateDbContext();

		dbContext.Games.Add(game);
		dbContext.SaveChanges();
	}

	public void RemoveGame(int gameId)
	{
		using var dbContext = statics.CreateDbContext();

		dbContext.Games.Remove(new Game { Id = gameId });
		dbContext.SaveChanges();
	}

	public void UpdateGame(int gameId, Game game)
	{
		using var dbContext = statics.CreateDbContext();

		var foundGame = dbContext.Games.First(g => g.Id == gameId);

		foundGame.Name = game.Name;
		foundGame.Price = game.Price;
		foundGame.Platform = game.Platform;
		foundGame.Genres = game.Genres;

		dbContext.SaveChanges();
	}

	public Game GetGame(int gameId)
	{
		using var dbContext = statics.CreateDbContext();

		return dbContext.Games.First(g => g.Id == gameId);
	}

	public IEnumerable<Game> GetGames()
	{
		using var dbContext = statics.CreateDbContext();

		return dbContext.Games.ToArray();
	}

	public void AddGenre(Genre genre)
	{
		using var dbContext = statics.CreateDbContext();

		dbContext.Genres.Add(genre);
		dbContext.SaveChanges();
	}

	public IEnumerable<Genre> GetGenres()
	{
		using var dbContext = statics.CreateDbContext();

		return dbContext.Genres.ToArray();
	}
}