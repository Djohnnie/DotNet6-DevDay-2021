using System.Text;
using System.Text.Json;



using Stream stream = Console.OpenStandardOutput();

var people = StreamPeople();
await JsonSerializer.SerializeAsync(stream, people);

Console.WriteLine();


var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("[0,1,2,3,4]"));
await foreach (var number in JsonSerializer.DeserializeAsyncEnumerable<int>(memoryStream))
{
    Console.WriteLine(number);
    if (Console.ReadKey().Key == ConsoleKey.Escape)
    {
        break;
    }
}


static async IAsyncEnumerable<Person> StreamPeople()
{
    for (byte i = 1; i < 20; i++)
    {
        yield return new Person { Name = $"Person-{i}", Age = i };
        await Task.Delay(1000);
    }
}

record Person
{
    public string Name { get; init; }
    public byte Age { get; init; }
}