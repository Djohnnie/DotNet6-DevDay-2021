
var list = new List<string>
{
    "Item 1","Item 2","Item 3",
    "Item 4","Item 5","Item 6",
    "Item 7","Item 8","Item 9"
};

var options = new ParallelOptions
{
    MaxDegreeOfParallelism = 3
};

await Parallel.ForEachAsync(list, options, async (item, _) =>
{
    await Task.Delay(1000);
    Console.WriteLine($"{item} @ {DateTime.Now:HH:mm:ss}");
});

Console.WriteLine();
Console.WriteLine("---------------");
Console.WriteLine();

Parallel.ForEach(list, options, async (item, _) =>
{
    await Task.Delay(1000);
    Console.WriteLine($"{item} @ {DateTime.Now:HH:mm:ss}");
});

Console.ReadKey();