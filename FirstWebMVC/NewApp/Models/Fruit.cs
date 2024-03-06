using System;

public class Fruit
{
    // Properties
    public string Name { get; set; }
    public string Color { get; set; }
    public decimal Price { get; set; }
    public string Origin { get; set; }

    // Constructor
    public Fruit(string name, string color, decimal price, string origin)
    {
        Name = name;
        Color = color;
        Price = price;
        Origin = origin;
    }

    // Method to enter information
    public void EnterInformation()
    {
        Console.Write("Enter Fruit Name: ");
        Name = Console.ReadLine();
        
        Console.Write("Enter Fruit Color: ");
        Color = Console.ReadLine();
        
        Console.Write("Enter Fruit Price: ");
        Price = decimal.Parse(Console.ReadLine());
        
        Console.Write("Enter Fruit Origin: ");
        Origin = Console.ReadLine();
    }

    // Method to display information
    public void DisplayInformation()
    {
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Color: {Color}");
        Console.WriteLine($"Price: {Price:C}");
        Console.WriteLine($"Origin: {Origin}");
    }
}
