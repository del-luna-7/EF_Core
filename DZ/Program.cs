using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ConsoleApp4;

class Program
{
    public static void Main()
    {
        var repository = new CarRepository();

        repository.AddCar("Ford", "Mustang", 2023, 45000);
        Console.WriteLine("Car added!");

        var cars = repository.Show();
        Console.WriteLine("Car list:");
        foreach (var car in cars)
        {
            Console.WriteLine($"{car.Id}: {car.Brand} {car.Model} - {car.Year} - {car.Price} $");
        }

        repository.UpdatePrice(1, 42000);
        Console.WriteLine("Price updated!");

        repository.Delete(1);
        Console.WriteLine("Car deleted!");
    }
}