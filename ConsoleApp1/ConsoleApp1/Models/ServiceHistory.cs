namespace ConsoleApp1.Models;

public class ServiceHistory
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public DateTime ServiceDate { get; set; }
    public string Description { get; set; } = null!;
    public decimal Cost { get; set; }
    public Car Car { get; set; } = null!;
}