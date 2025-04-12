namespace ConsoleApp1.Models;

public class Car
{
    public int Id { get; set; }
    public string Make { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }
    public decimal Price { get; set; }
    public int CarTypeId { get; set; }
    public CarType CarType { get; set; } = null!;
    public List<ServiceHistory> ServiceHistories { get; set; } = new();
}