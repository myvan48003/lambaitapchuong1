using System;
using NewApp.Models;

public class Student:Person
{
    // Properties
    public int StudentID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Course { get; set; }
    

    // Constructor
    public Student(int studentID, string name, int age, string course)
    {
        StudentID = studentID;
        Name = name;
        Age = age;
        Course = course;
    }

    public Student()
    {
        StudentID = -1;
        Name = "";
        Age = -1;
        Course = "";
    }

    // Method to enter information
    public void EnterInformation()
    {
        Console.Write("Enter Student ID: ");
        StudentID = int.Parse(Console.ReadLine());
        
        Console.Write("Enter Name: ");
        Name = Console.ReadLine();
        
        Console.Write("Enter Age: ");
        Age = int.Parse(Console.ReadLine());
        
        Console.Write("Enter Course: ");
        Course = Console.ReadLine();
    }

    // Method to display information
    public void DisplayInformation()
    {
        Console.WriteLine($"Student ID: {StudentID}");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Age: {Age}");
        Console.WriteLine($"Course: {Course}");
    }
}