using FizzWare.NBuilder;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Console;
using NewtonsoftSerializer = Newtonsoft.Json.JsonConvert;

namespace _06_NewJsonFeatures.Performance;

class Program
{
    static void Main(string[] args)
    {
        WriteLine("Preparing data...");

        var data = Builder<DataPoint>.CreateListOfSize(100)
            .All()
            .With(p => p.Id = Guid.NewGuid())
            .With(p => p.Title = Faker.Identification.UkNationalInsuranceNumber())
            .With(p => p.Description = Faker.Lorem.Sentence(100))
            .With(p => p.LeftData = GenerateDataPoint(0))
            .With(p => p.RightData = GenerateDataPoint(0))
            .Build().ToArray();

        for (int i = 0; i < 3; i++)
        {
            var sw1 = Stopwatch.StartNew();
            string newtonsoftJson = NewtonsoftSerializer.SerializeObject(data);
            sw1.Stop();
            WriteLine($"Newtonsoft JSON serialization:   {sw1.ElapsedMilliseconds}ms");
            sw1.Restart();
            _ = NewtonsoftSerializer.DeserializeObject<DataPoint[]>(newtonsoftJson);
            sw1.Stop();
            WriteLine($"Newtonsoft JSON deserialization: {sw1.ElapsedMilliseconds}ms");

            var sw2 = Stopwatch.StartNew();
            string builtinJson = JsonSerializer.Serialize(data);
            sw2.Stop();
            WriteLine($"Built-in JSON serialization:     {sw2.ElapsedMilliseconds}ms");

            sw2.Restart();
            _ = JsonSerializer.Deserialize<DataPoint[]>(builtinJson);
            sw2.Stop();
            WriteLine($"Built-in JSON deserialization:   {sw2.ElapsedMilliseconds}ms");

            var sw3 = Stopwatch.StartNew();
            string generatedJson = JsonSerializer.Serialize(data, DataPointContext.Default.DataPointArray);
            sw3.Stop();
            WriteLine($"Generated JSON serialization:    {sw3.ElapsedMilliseconds}ms");

            sw3.Restart();
            _ = JsonSerializer.Deserialize(generatedJson, DataPointContext.Default.DataPointArray);
            sw3.Stop();
            WriteLine($"Generated JSON deserialization:  {sw3.ElapsedMilliseconds}ms");

            WriteLine();
        }


    }





    static DataPoint GenerateDataPoint(int level)
    {
        if (level < 10)
        {
            return new DataPoint
            {
                Id = Guid.NewGuid(),
                Title = Faker.Identification.UkNationalInsuranceNumber(),
                Description = Faker.Lorem.Sentence(10),
                LeftData = GenerateDataPoint(level + 1),
                RightData = GenerateDataPoint(level + 1)
            };
        }

        return null;
    }
}

public partial class DataPoint
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DataPoint LeftData { get; set; }
    public DataPoint RightData { get; set; }
}

[JsonSerializable(typeof(DataPoint))]
[JsonSerializable(typeof(DataPoint[]))]
internal partial class DataPointContext : JsonSerializerContext
{

}