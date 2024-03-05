namespace NewApp.Models
{
    public class Person
    {

        public string FullName { get; set; }
        public string Address { get; set; }

        public void EnterData()
        {
            System.Console.Write("Full name = ");
            FullName = System.Console.ReadLine();
            System.Console.Write("Address = ");
            Address = System.Console.ReadLine();
        }

        public void Display()
        {
            System.Console.WriteLine("{0} - {1}", FullName, Address);
        }
        public void Display2(string ten, int tuoi)
    {
    System.Console.WriteLine("Sinh vien {0} - {1} tuoi", ten, tuoi);
    }
    public int GetYearOfBirth(int age)
    {
    int yearOfBirth = 2023 - age;
    return yearOfBirth;
    }
    
    }
}

