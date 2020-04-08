using System;
using Cythral.CodeGeneration.Roslyn;

namespace PackageConsumer
{
    [CodeGenerationAttribute("Sample.Generator.IdGenerator, PackagedGenerator")]
    class IdAttribute : Attribute { }

    [CodeGenerationAttribute("Sample.Generator.AddTargetFrameworkPropertyGenerator, PackagedGenerator")]
    class AddTargetFrameworkPropertyAttribute : Attribute { }

    [Id]
    [AddTargetFrameworkProperty]
    partial class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generated 'Id' property value: " + new Program().Id);
            Console.WriteLine("Generated 'TargetFramework' property value: " + new Program().TargetFramework);
        }
    }
}
