using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ConsoleApp4;

public class CarRepository
{
    private readonly string _connectionString = "Server=localhost; Database=Cars; Integrated Security=True; TrustServerCertificate=True;";

    public void AddCar(string brand, string model, int year, decimal price)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "INSERT INTO Cars (Brand, Model, Year, Price) VALUES (@Brand, @Model, @Year, @Price)";
        connection.Execute(sql, new { Brand = brand, Model = model, Year = year, Price = price });
    }

    public void UpdatePrice(int id, decimal newPrice)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "UPDATE Cars SET Price = @Price WHERE Id = @Id";
        connection.Execute(sql, new { Id = id, Price = newPrice });
    }

    public void Delete(int id)
    {
        using var connection = new SqlConnection(_connectionString);
        var sql = "DELETE FROM Cars WHERE Id = @Id";
        connection.Execute(sql, new { Id = id });
    }

    public List<Car> Show()
    {
        using var connection = new SqlConnection(_connectionString);
        return connection.Query<Car>("SELECT * FROM Cars").AsList();
    }
}