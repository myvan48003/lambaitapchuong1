using NewApp.Models;

public class Program
{
    private static void Main(string[] args)
    {
        Student std = new Student();
        std.EnterData();
        std.Display();

        // Create an instance of the Person class
        Person ps = new Person();

        // Declare two variables and assign values
        string str = "Nguyen Thi My Van";
        int a = 22;
        Console.WriteLine("{0} sinh nam {1}", str, ps.GetYearOfBirth(a));
    }
}
