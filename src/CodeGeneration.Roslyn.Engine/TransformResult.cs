// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.txt file in the project root for full license information.

namespace CodeGeneration.Roslyn.Engine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// This contains the outputs of DocumentTransform.TransformAsync.
    /// </summary>
    public class TransformResult
    {
        /// <summary>Gets or sets the generated syntax tree.</summary>
        public SyntaxTree SyntaxTree { get; set; }

        /// <summary>Gets or sets a set of generator types that were used to generate syntax trees.</summary>
        public HashSet<Type> GeneratorTypesUsed { get; set; }
    }
}