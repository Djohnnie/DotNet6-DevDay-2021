
using var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

while (await timer.WaitForNextTickAsync())
{
    Console.WriteLine($"Timer EVENT @ {DateTime.Now:HH:mm:ss}");
}