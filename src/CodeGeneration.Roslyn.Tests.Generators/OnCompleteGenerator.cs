  // Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.txt file in the project root for full license information.

namespace Cythral.CodeGeneration.Roslyn.Tests.Generators
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class OnCompleteGenerator : ICodeGenerator
    {
        public string Test { get; set; }
        public OnCompleteGenerator(AttributeData attributeData)
        {
        }

        public Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsync(TransformationContext context, IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {
            var results = SyntaxFactory.List<MemberDeclarationSyntax>();
            Test = "test";
            return Task.FromResult(results);
        }

        public void OnComplete(OnCompleteContext context)
        {
            var fileName = Directory.GetFiles(context.IntermediateOutputDirectory, "CodeGenerationTests.*.cs")[0];

#pragma warning disable SA1118
            File.AppendAllText(fileName, @"
public partial class OnComplete
{
    public static readonly bool WasCalled = true;
    public static string Test = """ + Test + @""";
}
            ");
#pragma warning restore SA1118
        }
    }
}