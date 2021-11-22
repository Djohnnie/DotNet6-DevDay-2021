using _8_SourceGenerator.Attributes;
using _8_SourceGenerator.Generator.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace _8_SourceGenerator.Generator;

[Generator]
public class MyGenerator : ISourceGenerator
{
    public void Initialize(GeneratorInitializationContext context)
    {
        context.RegisterForSyntaxNotifications(() => new SyntaxReceiver());
    }

    public void Execute(GeneratorExecutionContext context)
    {
        try
        {
            if (!(context.SyntaxReceiver is SyntaxReceiver receiver))
                return;

            var compilation = context.Compilation;
            INamedTypeSymbol attributeSymbol = compilation.GetTypeByMetadataName(typeof(SwaggerClientAttribute).FullName);

            foreach (ClassDeclarationSyntax candidateClass in receiver.CandidateClasses)
            {
                var model = compilation.GetSemanticModel(candidateClass.SyntaxTree);
                var classSymbol = model.GetDeclaredSymbol(candidateClass);

                foreach (var attribute in classSymbol.GetAttributes())
                {
                    if (attribute.AttributeClass.Name == attributeSymbol.Name)
                    {
                        string swaggerDescriptorPath = null;

                        foreach (var argument in attribute.NamedArguments)
                        {
                            if (argument.Key == "SwaggerDescriptor")
                            {
                                swaggerDescriptorPath = $"{argument.Value.Value}";
                            }
                        }

                        if (!string.IsNullOrEmpty(swaggerDescriptorPath))
                        {
                            Debugger.Break();

                            foreach (AdditionalText file in context.AdditionalFiles)
                            {
                                if (file.Path.Contains(swaggerDescriptorPath))
                                {
                                    string json = file.GetText().ToString();
                                    JsonDocument jsonDoc = JsonDocument.Parse(json);
                                    var rootElement = jsonDoc.RootElement;
                                    var swaggerDescription = JsonHelper.ProcessRootElement(rootElement);

                                    foreach (var swaggerModel in swaggerDescription.Models)
                                    {
                                        StringBuilder modelBuilder = new StringBuilder();
                                        modelBuilder.Append("using System; ");
                                        modelBuilder.Append($"namespace {classSymbol.ContainingNamespace.ToDisplayString()} {{ public class {swaggerModel.Name} {{ ");

                                        foreach (var modelProperty in swaggerModel.Properties)
                                        {
                                            modelBuilder.Append($" public {modelProperty.Type} {modelProperty.Name} {{ get; set; }}");
                                        }

                                        modelBuilder.Append("} }");
                                        context.AddSource(swaggerModel.Name, SourceText.From(modelBuilder.ToString(), Encoding.UTF8));
                                    }

                                    StringBuilder interfaceBuilder = new StringBuilder();
                                    interfaceBuilder.Append("using System; ");
                                    interfaceBuilder.Append("using System.Threading.Tasks; ");
                                    interfaceBuilder.Append($"namespace {classSymbol.ContainingNamespace.ToDisplayString()} {{ public interface I{classSymbol.Name} {{ ");

                                    foreach (var endpoint in swaggerDescription.Endpoints)
                                    {
                                        interfaceBuilder.Append($" Task<{endpoint.Response}> {endpoint.Method}{endpoint.Name}(string baseUrl);");
                                    }

                                    interfaceBuilder.Append("} }");
                                    context.AddSource($"I{classSymbol.Name}", SourceText.From(interfaceBuilder.ToString(), Encoding.UTF8));

                                    StringBuilder httpClientBuilder = new StringBuilder();
                                    httpClientBuilder.Append("using System; ");
                                    httpClientBuilder.Append("using System.Threading.Tasks; ");
                                    httpClientBuilder.Append("using RestSharp; ");
                                    httpClientBuilder.Append($"namespace {classSymbol.ContainingNamespace.ToDisplayString()} {{ public partial class {classSymbol.Name} : I{classSymbol.Name} {{ ");

                                    foreach (var endpoint in swaggerDescription.Endpoints)
                                    {
                                        httpClientBuilder.Append($" public async Task<{endpoint.Response}> {endpoint.Method}{endpoint.Name}(string baseUrl) {{ ");
                                        httpClientBuilder.Append($" RestClient client = new RestClient(baseUrl);");
                                        httpClientBuilder.Append($" RestRequest request = new RestRequest(\"/{endpoint.Name.ToLower()}\", Method.GET);");
                                        httpClientBuilder.Append($" var result = await client.ExecuteAsync<{endpoint.Response}>(request);");
                                        httpClientBuilder.Append($" return result.Data;");
                                        httpClientBuilder.Append($" }} ");
                                    }

                                    httpClientBuilder.Append("} }");
                                    context.AddSource($"{classSymbol.Name}_Generated", SourceText.From(httpClientBuilder.ToString(), Encoding.UTF8));
                                }
                            }
                        }

                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debugger.Break();
            Debug.WriteLine(ex);
        }
    }
}

/// <summary>
/// Created on demand before each generation pass
/// </summary>
class SyntaxReceiver : ISyntaxReceiver
{
    public List<ClassDeclarationSyntax> CandidateClasses { get; } = new List<ClassDeclarationSyntax>();

    /// <summary>
    /// Called for every syntax node in the compilation, we can inspect the nodes and save any information useful for generation
    /// </summary>
    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        // any class with at least one attribute is a candidate for interface generation
        if (syntaxNode is ClassDeclarationSyntax classDeclarationSyntax && classDeclarationSyntax.AttributeLists.Count > 0)
        {
            CandidateClasses.Add(classDeclarationSyntax);
        }
    }
}