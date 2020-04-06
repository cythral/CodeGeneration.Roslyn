namespace CodeGeneration.Roslyn.Tests.Generators
{
    using System;
    using System.Diagnostics;
    using Validation;

    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    [CodeGenerationAttribute(typeof(OnCompleteGenerator))]
    [Conditional("CodeGeneration")]
    public class OnCompleteAttribute : Attribute
    {
    }
}