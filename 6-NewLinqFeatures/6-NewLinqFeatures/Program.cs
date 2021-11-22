
var a = Enumerable.Range(1, 5).ElementAt(^2);
Console.WriteLine(a);

var source = Enumerable.Range(1, 10);

var b1 = source.Take(5);
b1.ToConsole();
var b2 = source.Take(..5);
b2.ToConsole();

var c1 = source.Skip(5);
c1.ToConsole();
var c2 = source.Take(5..);
c2.ToConsole();

var d1 = source.Take(6).Skip(2);
d1.ToConsole();
var d2 = source.Take(2..6);
d2.ToConsole();

var e1 = source.Skip(6).Take(2);
e1.ToConsole();
var e2 = source.Take(6..8);
e2.ToConsole();

var f1 = source.TakeLast(2);
f1.ToConsole();
var f2 = source.Take(^2..);
f2.ToConsole();

var g1 = source.SkipLast(2);
g1.ToConsole();
var g2 = source.Take(..^2);
g2.ToConsole();

var h1 = source.TakeLast(6).SkipLast(2);
h1.ToConsole();
var h2 = source.Take(^6..^2);
h2.ToConsole();

var i1 = source.SkipLast(3).TakeLast(2);
i1.ToConsole();
var i2 = source.Take(^5..^3);
i2.ToConsole();



var j = source.SingleOrDefault(x => x == 5, defaultValue: -1);









var chunks = source.Chunk(2);
chunks.ToConsole();






var people = new List<Person>
{
    new() { Name = "Number-1", Age = 18 },
    new() { Name = "Number-2", Age = 36 },
    new() { Name = "Number-3", Age = 11 },
    new() { Name = "Number-4", Age = 82 },
};

Console.WriteLine(people.MinBy(x => x.Age));






var buffer = people.TryGetNonEnumeratedCount(out int count)
                ? new List<Person>(capacity: count) : new List<Person>();

foreach (var item in people)
{
    buffer.Add(item);
}





static class EnumerableExtensions
{
    public static void ToConsole<T>(this IEnumerable<T> source)
    {
        Console.WriteLine(string.Join(",", source.Select(x => $"{x}")));
    }
}


record Person
{
    public string Name { get; init; }
    public byte Age { get; init; }
}