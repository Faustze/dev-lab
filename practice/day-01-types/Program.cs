using System.Text.Json;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

// === property with validation ===
Counter cnt = new Counter(1);
cnt.Count = 5;
Console.WriteLine(cnt.Count);

// === record: value equality ===
var firstPersonMoney = new Money("rubles", 100);
var secondPersonMoney = new Money("rubles", 100);
Console.WriteLine(firstPersonMoney == secondPersonMoney);
Console.WriteLine(object.ReferenceEquals(firstPersonMoney, secondPersonMoney));

// === serialization: field vs property ===
var person = new Person();
person.name = "name";
person.Name = "John";
var jsonString = JsonSerializer.Serialize(person);
Console.WriteLine(jsonString);

class Counter
{
    private int _count;

    public int Count
    {
        get => _count;
        set
        {
            if (value < 0)
                throw new ArgumentException("You can't set a negative value");
            _count = value;
        }
    }

    public Counter(int count)
    {
        Count = count;
    }
}

public record Money(string Currency, int Amount);

class Person
{
    public string? name;

    public string? Name { get; set; }
}
