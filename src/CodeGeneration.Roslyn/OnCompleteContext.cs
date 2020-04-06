// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.txt file in the project root for full license information.

namespace CodeGeneration.Roslyn
{
    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class OnCompleteContext {
        public OnCompleteContext(string intermediateOuptutDirectory, IReadOnlyDictionary<string, string> buildProperties) {
            IntermediateOutputDirectory = intermediateOuptutDirectory;
            BuildProperties = buildProperties;
        }

        public string IntermediateOutputDirectory { get; }

        /// <summary>Gets a dictionary of build properties on the project being generated for.</summary>
        public IReadOnlyDictionary<string, string> BuildProperties { get; }
    }
}