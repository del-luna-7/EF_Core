using GameLibrary;
using GameLibrary.Entities;

public static class Ui
{
	static Ui()
	{
		_statics = Statics.Instance;
	}

	private static Statics _statics;
	private static User? _activeUser;

	private static bool AddUser()
	{
		string login, email;

		Console.WriteLine("Enter user login: ");

		login = Console.ReadLine()!;

		Console.WriteLine("Enter user email: ");

		email = Console.ReadLine()!;

		var user = new User
		{
			Login = login,
			Email = email,
			Balance = 0
		};

		var userManager = _statics.GetUserManager();

		try
		{
			var addedUser = userManager.RegisterUser(user);

			Console.WriteLine($"User registered. Id: {addedUser.Id}");

			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);

			return false;
		}
	}

	private static bool SetCurrentUser()
	{
		var userManager = _statics.GetUserManager();

		while (true)
		{
			Console.WriteLine("Enter user id: ");
			if (int.TryParse(Console.ReadLine()!, out var userId))
			{
				_activeUser = userManager.GetUser(userId);

				Console.WriteLine("User set!");

				return true;
			}

			Console.WriteLine("Invalid user id.");
		}
	}

	private static bool TopUpBalance()
	{
		if (_activeUser is null)
		{
			Console.WriteLine("Current user is not set.");

			return false;
		}


		var userManager = _statics.GetUserManager();

		while (true)
		{
			Console.WriteLine("Enter amount: ");
			if (decimal.TryParse(Console.ReadLine()!, out var balance))
			{
				try
				{
					userManager.TopUpBalance(_activeUser.Id, balance);

					Console.WriteLine("Done!");
				}
				catch (Exception ex)
				{
					Console.WriteLine(ex.Message);
					return false;
				}


				return true;
			}

			Console.WriteLine("Invalid amount specified. Try again.");
		}
	}

	private static bool ShowGames()
	{
		var gameManager = _statics.GetLibraryManager();

		foreach (var game in gameManager.GetGames())
		{
			ShowGame(game);
		}

		return true;
	}

	private static bool PurchaseGame()
	{
		if (_activeUser is null)
		{
			Console.WriteLine("Current user is not set.");

			return false;
		}

		var userLibraryManager = _statics.GetUserLibraryManager();

		userLibraryManager.User = _activeUser;

		Console.WriteLine("Enter game ids separated by space: ");

		var gameIds = Console.ReadLine()!.Split(' ').Select(x =>
		{
			if (int.TryParse(x, out var gameId))
			{
				return gameId;
			}

			return -1;
		}).Where(x => x != -1);


		try
		{
			userLibraryManager.PurchaseGame(gameIds);

			Console.WriteLine("Done!");

			return true;
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);

			return false;
		}
	}

	private static bool ShowHistory()
	{
		if (_activeUser is null)
		{
			Console.WriteLine("Current user is not set.");

			return false;
		}

		var userLibraryManager = _statics.GetUserLibraryManager();

		userLibraryManager.User = _activeUser;

		IEnumerable<Order> history;

		try
		{
			history = userLibraryManager.GetHistory();
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Failed to get history: {ex.Message}");

			return false;
		}

		foreach (var order in history)
		{
			Console.WriteLine($"Order id: {order.Id}");
			Console.WriteLine($"Total price: {order.TotalPrice}");

			foreach (var orderDetail in order.OrderDetails)
			{
				Console.WriteLine(new string('-', 16));
				Console.WriteLine($"Game Id: {orderDetail.Game.Id}");
				Console.WriteLine($"Game name: {orderDetail.Game.Name}");
			}
		}

		return true;
	}

	private static bool AddGenres()
	{
		var libraryManager = _statics.GetLibraryManager();

		Console.WriteLine("Specify genre names separated by space: ");

		var genres = Console.ReadLine()!.Split(' ').Select(x => new Genre
		{
			Name = x
		}).ToArray();

		foreach (var genre in genres)
		{
			try
			{
				libraryManager.AddGenre(genre);
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Failed to add genre {genre}: {ex.Message}");
			}
		}

		return true;
	}

	private static bool ShowGenres()
	{
		var libraryManager = _statics.GetLibraryManager();

		Console.WriteLine("List of genres: ");

		foreach (var genre in libraryManager.GetGenres())
		{
			Console.WriteLine($"Name: {genre.Name}, Id: {genre.Id}");
		}

		return true;
	}

	private static bool AddGame()
	{
		var libraryManager = _statics.GetLibraryManager();

		string name;
		var genres = new List<Genre>();
		GamePlatform platform;
		decimal price;


		Console.WriteLine("Enter game name: ");

		name = Console.ReadLine()!;

		Console.WriteLine("Specify genre ids separated by space (0 to stop): ");

		while (true)
		{
			if (int.TryParse(Console.ReadLine()!, out var genreId))
			{
				if (genreId == 0)
				{
					break;
				}

				genres.Add(new Genre
				{
					Id = genreId
				});
			}
		}

		Console.WriteLine("Specify platform (Pc, Xbox, Ps): ");

		while (true)
		{
			var input = Console.ReadLine()!;

			if (Enum.TryParse(typeof(GamePlatform), input, true, out var result))
			{
				platform = (GamePlatform)result;

				break;
			}
		}

		Console.WriteLine("Specify price: ");

		while (true)
		{
			if (!decimal.TryParse(Console.ReadLine()!, out price))
			{
				Console.WriteLine("Invalid price!");

				continue;
			}

			break;
		}

		try
		{
			libraryManager.AddGame(new Game
			{
				Genres = genres,
				Name = name,
				Platform = platform,
				Price = price
			});
		}
		catch (Exception ex)
		{
			Console.WriteLine($"Failed to add game: {ex.Message}");

			return false;
		}

		return true;
	}

	private static bool RemoveGame()
	{
		var libraryManager = _statics.GetLibraryManager();

		int gameId;

		Console.WriteLine("Specify game id to remove: ");

		while (true)
		{
			if (int.TryParse(Console.ReadLine()!, out gameId))
			{
				try
				{
					libraryManager.RemoveGame(gameId);
				}
				catch (Exception ex)
				{
					Console.WriteLine($"Failed to remove game: {ex.Message}");

					return false;
				}

				return true;
			}
		}
	}

	private static void ShowGame(Game game)
	{
		Console.WriteLine($"Id: {game.Id}");
		Console.WriteLine($"Name: {game.Name}");
		Console.WriteLine($"Platform: {game.Platform}");
		Console.WriteLine($"Price: {game.Price}");
		Console.Write("Genres: ");

		foreach (var genre in game.Genres)
		{
			Console.Write(genre.Name + " ");
		}

		Console.WriteLine();
	}

	public static void Run()
	{
		_statics.EnsureDbCreated();

		Console.WriteLine("1. Register user");
		Console.WriteLine("2. Set active user");
		Console.WriteLine("3. Top up balance");
		Console.WriteLine("4. Show games");
		Console.WriteLine("5. Purchase a game");
		Console.WriteLine("6. Show history");
		Console.WriteLine("7. Add game");
		Console.WriteLine("8. Remove game");
		Console.WriteLine("9. Show genres");
		Console.WriteLine("10. Add genres");
		Console.WriteLine("0. Exit");

		while (true)
		{
			if (int.TryParse(Console.ReadLine()!, out var action))
			{
				switch (action)
				{
					case 1:
						AddUser();
						break;
					case 2:
						SetCurrentUser();
						break;
					case 3:
						TopUpBalance();
						break;
					case 4:
						ShowGames();
						break;
					case 5:
						PurchaseGame();
						break;
					case 6:
						ShowHistory();
						break;
					case 7:
						AddGame();
						break;
					case 8:
						RemoveGame();
						break;
					case 9:
						ShowGenres();
						break;
					case 10:
						AddGenres();
						break;
					case 0:
						Console.WriteLine("Sayonara! <3");
						return;
				}
			}
		}
	}
}