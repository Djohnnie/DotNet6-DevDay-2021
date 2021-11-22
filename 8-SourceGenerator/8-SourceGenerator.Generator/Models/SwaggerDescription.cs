using System.Collections.Generic;

namespace _8_SourceGenerator.Generator.Models;

public class SwaggerDescription
{
    public List<EndpointDescription> Endpoints { get; } = new List<EndpointDescription>();
    public List<ModelDescription> Models { get; } = new List<ModelDescription>();
}