using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Apprendi.Application.Common.SourceGenerator
{
    [Generator]
    public class GenericServiceRegistrationGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            // Register a pipeline to gather all class declarations
            var classDeclarations = context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: static (node, _) => node is ClassDeclarationSyntax, 
                    transform: static (context, _) => (ClassDeclarationSyntax)context.Node) 
                .Where(static classDecl => classDecl is not null);

            var compilationAndClasses = context.CompilationProvider.Combine(classDeclarations.Collect());

            // Register the source output
            context.RegisterSourceOutput(compilationAndClasses, static (spc, source) =>
            {
                var (compilation, classes) = source;

                var queryResponsePairs = FindQueryResponsePairs(compilation, classes);

                var sourceCode = GenerateSource(queryResponsePairs);

                spc.AddSource("GenericServiceRegistrationGeneratorExtensions.g.cs", SourceText.From(sourceCode, Encoding.UTF8));
            });
        }

        private static List<(INamedTypeSymbol Query, INamedTypeSymbol Response)> FindQueryResponsePairs(
            Compilation compilation, IEnumerable<ClassDeclarationSyntax> classes)
        {
            var queryResponsePairs = new List<(INamedTypeSymbol Query, INamedTypeSymbol Response)>();

            foreach (var classDeclaration in classes)
            {
                var model = compilation.GetSemanticModel(classDeclaration.SyntaxTree);
                if (model.GetDeclaredSymbol(classDeclaration) is INamedTypeSymbol classSymbol)
                {
                    // Ensure the class implements IRequest<TResponse> and is not abstract
                    if (classSymbol.AllInterfaces.Any(i =>
                            i.Name == "IRequest" &&
                            i.IsGenericType) &&
                        !classSymbol.IsAbstract)
                    {
                        // Get the generic type argument (TResponse)
                        var requestInterface = classSymbol.AllInterfaces
                            .First(i => i.Name == "IRequest" && i.IsGenericType);
                        var responseType = requestInterface.TypeArguments[0];

                        // Add the Query-Response pair
                        if (responseType is INamedTypeSymbol responseSymbol)
                        {
                            queryResponsePairs.Add((classSymbol, responseSymbol));
                        }
                    }
                }
            }

            return queryResponsePairs;
        }

        private static string GenerateSource(List<(INamedTypeSymbol Query, INamedTypeSymbol Response)> queryResponsePairs)
        {
            var namespaces = queryResponsePairs
                .SelectMany(x => new[] { x.Response.ContainingNamespace, x.Query.ContainingNamespace })
                .ToImmutableHashSet(SymbolEqualityComparer.Default);

            var sourceBuilder = new StringBuilder();

            sourceBuilder.AppendLine("using Microsoft.Extensions.DependencyInjection;");
            sourceBuilder.AppendLine("using Microsoft.Extensions.DependencyInjection;");
            sourceBuilder.AppendLine("using MediatR;");
            sourceBuilder.AppendLine("using Apprendi.Application.Common.Behaviours;");
            sourceBuilder.AppendLine("using Apprendi.Application.Factories;");
            sourceBuilder.AppendLine("using Apprendi.Application.Common;");
            sourceBuilder.AppendLine(string.Join("\n", namespaces.Select(x => $"using {x};")));
            sourceBuilder.AppendLine();

            sourceBuilder.AppendLine("namespace Apprendi.Application.Common.Extensions");
            sourceBuilder.AppendLine("{");
            sourceBuilder.AppendLine("    public static class GeneratedPipelineBehaviorRegistrationExtensions");
            sourceBuilder.AppendLine("    {");
            sourceBuilder.AppendLine("        public static IServiceCollection AddGeneratedPipelineBehaviors(this IServiceCollection services)");
            sourceBuilder.AppendLine("        {");

            // Add pipeline registrations
            for (int i = 0; i < queryResponsePairs.Count; i++)
            {
                var (query, response) = queryResponsePairs[i];
                sourceBuilder.AppendLine($"            services.AddTransient<IPipelineBehavior<{query.Name}, {response.Name}>, UnhandledExceptionBehaviour<{query.Name}, {response.Name}>>();");
                sourceBuilder.AppendLine($"            services.AddTransient<IPipelineBehavior<{query.Name}, {response.Name}>, ValidationBehaviour<{query.Name}, {response.Name}>>();");
                sourceBuilder.AppendLine($"            services.AddTransient<IPipelineBehavior<{query.Name}, {response.Name}>, ValidationBehaviour<{query.Name}, {response.Name}>>();");
                sourceBuilder.AppendLine($"            services.AddTransient<IRequestHandler<{query.Name}, {response.Name}>, ApiRequestHandler<{query.Name}, {response.Name}>>();");
                sourceBuilder.AppendLine($"            services.AddTransient<IResponseFactory<{response.Name}>, ResponseFactory<{response.Name}>>();");

                if (i < queryResponsePairs.Count - 1)
                {
                    sourceBuilder.AppendLine();
                }
            }
            sourceBuilder.AppendLine("            return services;");
            sourceBuilder.AppendLine("        }");
            sourceBuilder.AppendLine("    }");
            sourceBuilder.AppendLine("}");

            return sourceBuilder.ToString();
        }
    }
}
