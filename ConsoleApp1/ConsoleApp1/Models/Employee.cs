namespace ConsoleApp1.Models;

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Position { get; set; } = null!;
    public List<Sale> Sales { get; set; } = new();
}