using System;

public class Employee
{
    // Properties
    public int EmployeeID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public decimal Salary { get; set; }

    // Constructor
    public Employee(int employeeID, string name, int age, decimal salary)
    {
        EmployeeID = employeeID;
        Name = name;
        Age = age;
        Salary = salary;
    }

    // Method to enter information
    public void EnterInformation()
    {
        Console.Write("Enter Employee ID: ");
        EmployeeID = int.Parse(Console.ReadLine());
        
        Console.Write("Enter Name: ");
        Name = Console.ReadLine();
        
        Console.Write("Enter Age: ");
        Age = int.Parse(Console.ReadLine());
        
        Console.Write("Enter Salary: ");
        Salary = decimal.Parse(Console.ReadLine());
    }

    // Method to display information
    public void DisplayInformation()
    {
        Console.WriteLine($"Employee ID: {EmployeeID}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine($"Salary: {Salary:C}"); // ':C' formats the number as a currency
    }
}
