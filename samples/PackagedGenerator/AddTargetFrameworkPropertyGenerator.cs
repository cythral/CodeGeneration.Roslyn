using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Cythral.CodeGeneration.Roslyn;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Sample.Generator
{
    public class AddTargetFrameworkPropertyGenerator : ICodeGenerator
    {
        public AddTargetFrameworkPropertyGenerator(AttributeData attributeData)
        {
        }

        public Task<SyntaxList<MemberDeclarationSyntax>> GenerateAsync(TransformationContext context, IProgress<Diagnostic> progress, CancellationToken cancellationToken)
        {
            var partialType = CreatePartialType();
            return Task.FromResult(SyntaxFactory.List(partialType));

            IEnumerable<MemberDeclarationSyntax> CreatePartialType()
            {
                var newPartialType = 
                    context.ProcessingNode is ClassDeclarationSyntax classDeclaration
                        ? SyntaxFactory.ClassDeclaration(classDeclaration.Identifier.ValueText)
                        : context.ProcessingNode is StructDeclarationSyntax structDeclaration
                            ? SyntaxFactory.StructDeclaration(structDeclaration.Identifier.ValueText)
                            : default(TypeDeclarationSyntax);
                if (newPartialType is null)
                    yield break;
                yield return newPartialType
                    ?.AddModifiers(SyntaxFactory.Token(SyntaxKind.PartialKeyword))
                    .AddMembers(CreateTargetFrameworkProperty());
            }
            MemberDeclarationSyntax CreateTargetFrameworkProperty()
            {
                var value = context.BuildProperties["TargetFramework"];
                return SyntaxFactory.ParseMemberDeclaration($"public string TargetFramework {{ get; }} = \"{value}\";");
            }
        }
    }
}