using CodeFirst.Data.Contexts;
using ConsoleApp1;

class Program
{
    static void Main()
    {
        using (var context = new ShowroomContext())
        {
            DatabaseSeeder.Seed(context);
            var queries = new LINQ(context);
            foreach (var car in queries.GetCars(1))
            {
                Console.WriteLine(car);
            }
        }
    }
}