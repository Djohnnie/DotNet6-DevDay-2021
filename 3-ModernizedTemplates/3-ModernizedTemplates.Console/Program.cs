Console.WriteLine("Hello, World!");

var client = new RestClient("https://httpbin.org/get");
var request = new RestRequest(Method.GET);
var response = await client.GetAsync<string>(request);
Console.WriteLine(response);

Console.WriteLine("Press any key to shut down...");
Console.ReadKey();