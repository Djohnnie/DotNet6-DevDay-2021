
var input = Convert.ToInt32(Console.ReadLine());

Console.WriteLine(input);

Task result = File.ReadAllTextAsync(@"c:\somepath.txt");
await result;

Dictionary<int, Tuple<bool, string>> dictionary = new()
{
    { 0, new(true, "Zero") },
    { 1, new(false, "One") },
    { 2, new(true, "Two") },
    { 3, new(false, "Three") },
    { 4, new(true, "Four") },
};