﻿namespace ConsoleApp1.Models;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public List<Sale> Sales { get; set; } = new();
}