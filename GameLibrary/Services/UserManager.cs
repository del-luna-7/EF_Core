using GameLibrary.Entities;

namespace GameLibrary.Services;

public sealed class UserManager(Statics statics) : IUserManager
{
	public User RegisterUser(User user)
	{
		using var dbContext = statics.CreateDbContext();

		var entry = dbContext.Users.Add(user);

		dbContext.SaveChanges();

		return entry.Entity;
	}

	public User GetUser(int userId)
	{
		using var dbContext = statics.CreateDbContext();

		return dbContext.Users.First(u => u.Id == userId);
	}

	public void TopUpBalance(int userId, decimal amount)
	{
		using var dbContext = statics.CreateDbContext();

		var user = dbContext.Users.First(u => u.Id == userId);

		if (amount > 0)
		{
			user.Balance += amount;
		}

		dbContext.SaveChanges();
	}
}