
List<string> data = new()
{
    "one",
    "two",
    "three",
    "four",
    "five",
    "six",
    "seven",
    "eight",
    "nine"
};

string zero = data.SingleOrDefault(x => x == "zero");
string? one = data.SingleOrDefault(x => x == "one");
var two = data.SingleOrDefault(x => x == "two");
var three = data.SingleOrDefault(x => x == "three") ?? "THREE";

Console.WriteLine($"{zero.Length} - {zero}");
Console.WriteLine($"{one.Length} - {one}");
Console.WriteLine($"{two.Length} - {two}");
Console.WriteLine($"{three.Length} - {three}");