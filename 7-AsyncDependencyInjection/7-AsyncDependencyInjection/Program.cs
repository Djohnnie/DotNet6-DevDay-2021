
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddScoped<MyService>();
var provider = services.BuildServiceProvider();

using (var scope = provider.CreateScope())
{
    var service = scope.ServiceProvider.GetService<MyService>();
}

await using (var scope = provider.CreateAsyncScope())
{
    var service = scope.ServiceProvider.GetService<MyService>();
}




class MyService : IAsyncDisposable
{
    public async ValueTask DisposeAsync()
    {
        await Task.Delay(1000);
    }
}