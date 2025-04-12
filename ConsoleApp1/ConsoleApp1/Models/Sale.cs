namespace ConsoleApp1.Models;

public class Sale
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public DateTime SaleDate { get; set; }
    public decimal FinalPrice { get; set; }
    public Car Car { get; set; } = null!;
    public Customer Customer { get; set; } = null!;
    public Employee Employee { get; set; } = null!;
}