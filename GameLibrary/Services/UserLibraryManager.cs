using GameLibrary.Entities;

namespace GameLibrary.Services;

public sealed class UserLibraryManager(Statics statics) : IUserLibraryManager
{
	public User? User { get; set; }

	public void PurchaseGame(IEnumerable<int> gameIds)
	{
		if (User is null)
		{
			throw new InvalidOperationException("User is not set.");
		}

		using var dbContext = statics.CreateDbContext();

		var order = new Order
		{
			Timestamp = DateTime.UtcNow,
			TotalPrice = 0,
			UserId = User.Id
		};
		var userEntity = dbContext.Users.First(u => u.Id == User.Id);
		var orderEntry = dbContext.Orders.Add(order);

		dbContext.SaveChanges();

		foreach (var gameId in gameIds)
		{
			var game = dbContext.Games.First(g => g.Id == gameId);

			if (userEntity.Balance <= game.Price)
			{
				orderEntry.Entity.OrderDetails.Add(new OrderDetails
				{
					GameId = game.Id,
					OrderId = orderEntry.Entity.Id,
					Quantity = 1
				});

				order.TotalPrice += game.Price;
				userEntity.Balance -= game.Price;
			}
		}

		dbContext.SaveChanges();
	}

	public IEnumerable<Order> GetHistory()
	{
		if (User is null)
		{
			return [];
		}

		using var dbContext = statics.CreateDbContext();

		return dbContext.Orders.Where(o => o.UserId == User.Id).ToArray();
	}
}