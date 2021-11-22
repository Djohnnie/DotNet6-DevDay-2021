using System.Text.Json.Nodes;

var node = JsonNode.Parse("{\"Name\": \"John\", \"Age\": 36}");
var age = node["Age"].GetValue<byte>();

var biggerNode = new JsonObject
{
    ["Name"] = "Johnny",
    ["Age"] = 36,
    ["Address"] = new JsonObject
    {
        ["Street"] = "Molestreet",
        ["Numbers"] = new JsonArray(1022, 1024, 1026)
    }
};

Console.WriteLine(biggerNode["Address"]["Numbers"][1].GetValue<int>() == 1024);
