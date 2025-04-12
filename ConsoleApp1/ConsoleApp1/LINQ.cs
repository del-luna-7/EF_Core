using CodeFirst.Data.Contexts;
using ConsoleApp1.Models;

namespace ConsoleApp1;

public class LINQ
{
    private readonly ShowroomContext _context;

    public LINQ(ShowroomContext context)
    {
        _context = context;
    }

    public List<Car> GetCars(int customerId)
    {
        return _context.Sales.Where(x => x.CustomerId == customerId).Select(x => x.Car).ToList();
    }

    public List<Sale> GetSales(DateTime startDate, DateTime endDate)
    {
        return _context.Sales.Where(x => x.SaleDate >= startDate && x.SaleDate <= endDate).ToList();
    }
}