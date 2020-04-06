  
namespace CodeGeneration.Roslyn.Tests.Generators
{
    using System;
    using System.IO;
    using System.Diagnostics;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class OnCompleteGenerator : ICodeGenerator
    {
        public static bool OnCompleteCalled { get; private set; } = false;

        public OnCompleteGenerator(AttributeData attributeData)
        {
        }

        public Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsync(TransformationContext context, IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();
            return Task.FromResult(results);
        }

        public static void OnComplete(CompilationGenerator context)
        {
            var fileName = Directory.GetFiles(context.IntermediateOutputDirectory, "CodeGenerationTests.*.cs")[0];
            File.AppendAllText(fileName, @"
public partial class OnComplete
{
    public static readonly bool WasCalled = true;
}
            ");
        }
    }
}