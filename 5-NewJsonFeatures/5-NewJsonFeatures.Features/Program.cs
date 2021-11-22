using System.Text.Json;
using System.Text.Json.Serialization;

try
{
    var serializedPerson1 = JsonSerializer.Serialize(new Person { Name = "John", Age = 36 }, new JsonSerializerOptions { WriteIndented = true });
    Console.WriteLine(serializedPerson1);
    var person1 = JsonSerializer.Deserialize<Person>(serializedPerson1);

    var serializedPerson2 = JsonSerializer.Serialize(new Person { Age = 36 });
    var person2 = JsonSerializer.Deserialize<Person>(serializedPerson2);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

public class Person : IJsonOnDeserialized, IJsonOnSerializing
{
    public string NickName { get; set; }

    [JsonPropertyOrder(1)]
    public string Name { get; set; }

    [JsonPropertyOrder(-1)]
    public byte Age { get; set; }

    void IJsonOnDeserialized.OnDeserialized()
    {
        Validate();
        Console.WriteLine($"{nameof(Person)} DESERIALIZED!");
    }
    void IJsonOnSerializing.OnSerializing()
    {
        Validate();
        Console.WriteLine($"{nameof(Person)} SERIALIZED!");
    }

    private void Validate()
    {
        if (Name is null)
        {
            throw new InvalidOperationException(
                $"The '{nameof(Name)}' property cannot be 'null'.");
        }
    }
}