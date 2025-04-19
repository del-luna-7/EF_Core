using GameLibrary.Entities;

namespace GameLibrary.Services;

public interface IUserLibraryManager
{
	User? User { get; set; }

	void PurchaseGame(IEnumerable<int> gameIds);
	IEnumerable<Order> GetHistory();
}