using GameLibrary.Data;
using GameLibrary.Entities;

namespace GameLibrary.Services;

public interface ILibraryManager
{
	void AddGame(Game game);

	void RemoveGame(Game game)
	{
		RemoveGame(game.Id);
	}

	void RemoveGame(int gameId);
	void UpdateGame(int gameId, Game game);
	Game GetGame(int gameId);
	IEnumerable<Game> GetGames();

	void AddGenre(Genre genre);
	IEnumerable<Genre> GetGenres();
}