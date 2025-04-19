namespace GameLibrary.Entities;

public class User
{
	public int Id { get; set; }
	public string Login { get; set; } = null!;
	public string Email { get; set; } = null!;
	public decimal Balance { get; set; }
	public ICollection<Order> Orders { get; set; } = [];
}