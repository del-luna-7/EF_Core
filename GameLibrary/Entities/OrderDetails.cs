namespace GameLibrary.Entities;

public class OrderDetails
{
	public int OrderId { get; set; }
	public Order Order { get; set; } = null!;
	public int GameId { get; set; }
	public Game Game { get; set; } = null!;
	public int Quantity { get; set; }
}