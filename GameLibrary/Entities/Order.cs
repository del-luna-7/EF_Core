namespace GameLibrary.Entities;

public class Order
{
	public int Id { get; set; }
	public User User { get; set; } = null!;
	public int UserId { get; set; }
	public DateTime Timestamp { get; set; }
	public decimal TotalPrice { get; set; }

	public ICollection<OrderDetails> OrderDetails { get; set; } = [];
}