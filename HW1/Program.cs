using System.Globalization;

public class Person
{
    private string name;
    private string address;
    private double salary;

    public Person(string name, string address, double salary)
    {
        this.name = name;
        this.address = address;
        this.salary = salary;
    }

    public string Name
    {
        get => name;
        set => name = value;
    }

    public string Address
    {
        get => address;
        set => address = value;
    }

    public double Salary
    {
        get => salary;
        set => salary = value;
    }

    public static Person inputPersonInfo(string name, string address, string sSalary)
    {
        if (string.IsNullOrWhiteSpace(sSalary))
        {
            throw new Exception("You must input Salary.");
        }

        if (!double.TryParse(sSalary, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsedSalary))
        {
            throw new Exception("You must input digit.");
        }

        if (parsedSalary <= 0)
        {
            throw new Exception("Salary is greater than zero");
        }

        return new Person(name, address, parsedSalary);
    }

    public static void displayPersonInfo(Person person)
    {
        if (person == null)
        {
            throw new ArgumentNullException(nameof(person));
        }

        Console.WriteLine("Information of Person you have entered:");
        Console.WriteLine($"Name:{person.Name}");
        Console.WriteLine($"Address:{person.Address}");
        Console.WriteLine($"Salary:{person.Salary.ToString("0.0", CultureInfo.InvariantCulture)}");
    }

    public static Person[] sortBySalary(Person[] person)
    {
        if (person == null)
        {
            throw new Exception("Can't Sort Person");
        }

        var result = (Person[])person.Clone();
        int n = result.Length;

        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (result[j].Salary > result[j + 1].Salary)
                {
                    var temp = result[j];
                    result[j] = result[j + 1];
                    result[j + 1] = temp;
                }
            }
        }

        return result;
    }
}

internal class Program
{
    private static void Main()
    {
        Console.WriteLine("=====Management Person programer=====");

        var persons = new Person[3];

        for (int i = 0; i < persons.Length; i++)
        {
            Console.WriteLine("Input Information of Person");
            Console.Write("Please input name:");
            var name = Console.ReadLine() ?? string.Empty;
            Console.Write("Please input address:");
            var address = Console.ReadLine() ?? string.Empty;

            while (true)
            {
                Console.Write("Please input salary:");
                var inputSalary = Console.ReadLine() ?? string.Empty;

                try
                {
                    persons[i] = Person.inputPersonInfo(name, address, inputSalary);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        var sortedPersons = Person.sortBySalary(persons);

        foreach (var person in sortedPersons)
        {
            Person.displayPersonInfo(person);
            Console.WriteLine();
        }
    }
}
