using System;

namespace _8_SourceGenerator.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class SwaggerClientAttribute : Attribute
{
    public string SwaggerDescriptor { get; set; }
}