// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.txt file in the project root for full license information.

// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.txt file in the project root for full license information.

namespace CodeGeneration.Roslyn
{

    using System.Collections.Generic;
    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    /// <summary>
    /// Provides all the inputs and context necessary for post-generation processing.
    /// </summary>
    public class OnCompleteContext 
    {

        /// <summary>
        /// Initializes a new instance of the OnCompleteContext class.
        /// </summary>
        /// <param name="intermediateOutputDirectory">The intermediate output directory of the project being generated for.</param>
        /// <param name="buildProperties">A read only dictionary of build properties that were present in the project being generated for.</param>
        public OnCompleteContext(string intermediateOutputDirectory, IReadOnlyDictionary<string, string> buildProperties) 
        {
            IntermediateOutputDirectory = intermediateOutputDirectory;
            BuildProperties = buildProperties;
        }

        /// <summary>Gets the intermediate output directory of the project being generated for.</summary>
        public string IntermediateOutputDirectory { get; }

        /// <summary>Gets a dictionary of build properties on the project being generated for.</summary>
        public IReadOnlyDictionary<string, string> BuildProperties { get; }
    }
}