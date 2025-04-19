using GameLibrary.Entities;

namespace GameLibrary.Services;

public interface IUserManager
{
	User RegisterUser(User user);
	
	User GetUser(int userId);

	void TopUpBalance(User user, decimal amount)
	{
		TopUpBalance(user.Id, amount);
	}

	void TopUpBalance(int userId, decimal amount);
}