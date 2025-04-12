using CodeFirst.Data.Contexts;
using ConsoleApp1.Models;

namespace ConsoleApp1;

public class DatabaseSeeder
{
    public static void Seed(ShowroomContext context)
    {
        if (!context.CarTypes.Any())
        {
            context.CarTypes.AddRange(
                new CarType{ Name = "Седан"},
                new CarType{ Name = "Внедорожник"}
                );
            context.SaveChanges();
        }
        
        if (!context.Cars.Any())
        {
            context.Cars.AddRange(
                new Car{ Make = "Mitsubishi", Model = "Lancer", CarTypeId = 1, Year = 2020, Price = 50000},
                new Car{ Make = "Toyota", Model = "Cruiser", CarTypeId = 2, Year = 2024, Price = 70000}
            );
            context.SaveChanges();
        }
        
        if (!context.Customers.Any())
        {
            context.Customers.AddRange(
                new Customer{ Name = "Huseyn", Email = "gusain208@gmail.com", Phone = "12345678" }
            );
            context.SaveChanges();
        }
        
        if (!context.Employees.Any())
        {
            context.Employees.AddRange(
                new Employee{ Name = "Rustam", Position = "Sells Manager"}
            );
            context.SaveChanges();
        }
        
        if (!context.Sales.Any())
        {
            context.Sales.AddRange(
                new Sale{ CarId = 1, CustomerId = 1, EmployeeId = 1, SaleDate = DateTime.Now, FinalPrice = 50000}
            );
            context.SaveChanges();
        }
    }
}