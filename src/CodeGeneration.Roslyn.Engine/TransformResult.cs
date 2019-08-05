namespace CodeGeneration.Roslyn.Engine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class TransformResult
    {
        public SyntaxTree GeneratedSyntaxTree;
        public HashSet<Type> GeneratorTypesUsed;
    }
}