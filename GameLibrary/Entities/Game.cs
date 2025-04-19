namespace GameLibrary.Entities;

public class Game
{
	public int Id { get; set; }
	public string Name { get; set; } = null!;
	public ICollection<Genre> Genres { get; set; } = [];
	public GamePlatform Platform { get; set; }
	public decimal Price { get; set; }

	public ICollection<OrderDetails> OrderDetails { get; set; } = null!;
}