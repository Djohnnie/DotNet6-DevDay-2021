
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MapGet("/", () => "Hello World!");
app.MapGet("/person", () => new Person { Name = "Johny", Age = 36 } );

app.Run();

class Person
{
    public string Name { get; set; }
    public byte Age { get; set; }
}