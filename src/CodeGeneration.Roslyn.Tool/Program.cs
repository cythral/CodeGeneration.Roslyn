// Copyright (c) Andrew Arnott. All rights reserved.
// Licensed under the MS-PL license. See LICENSE.txt file in the project root for full license information.

namespace CodeGeneration.Roslyn.Generate
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using CodeGeneration.Roslyn;
    using CodeGeneration.Roslyn.Engine;
    using CodeGeneration.Roslyn.Tool.CommandLine;
    using Microsoft.CodeAnalysis;

    public static class Program
    {
        public static int Main(string[] args)
        {
            IReadOnlyList<string> compile = Array.Empty<string>();
            IReadOnlyList<string> refs = Array.Empty<string>();
            IReadOnlyList<string> preprocessorSymbols = Array.Empty<string>();
            IReadOnlyList<string> generatorSearchPaths = Array.Empty<string>();
            IReadOnlyList<string> buildPropertyList = Array.Empty<string>();
            Dictionary<string,string> buildProperties = new Dictionary<string,string>();
            string generatedCompileItemFile = null;
            string outputDirectory = null;
            string projectDir = null;
            string assemblyName = null;
            bool version = false;
            ArgumentSyntax.Parse(args, syntax =>
            {
                syntax.DefineOption("version", ref version, "Show version of this tool (and exits).");
                syntax.DefineOptionList("r|reference", ref refs, "Paths to assemblies being referenced");
                syntax.DefineOptionList("d|define", ref preprocessorSymbols, "Preprocessor symbols");
                syntax.DefineOptionList("generatorSearchPath", ref generatorSearchPaths, "Paths to folders that may contain generator assemblies");
                syntax.DefineOptionList("buildProperty", ref buildPropertyList, "MSBuild properties to expose to the generator");
                syntax.DefineOption("out", ref outputDirectory, true, "The directory to write generated source files to");
                syntax.DefineOption("projectDir", ref projectDir, true, "The absolute path of the directory where the project file is located");
                syntax.DefineOption("generatedFilesList", ref generatedCompileItemFile, "The path to the file to create with a list of generated source files");
                syntax.DefineOption("assemblyName", ref assemblyName, "Name of the assembly in which the source files are being compiled into");
                syntax.DefineParameterList("compile", ref compile, "Source files included in compilation");
            });
            
            if (version)
            {
                Console.WriteLine(ThisAssembly.Version);
                return 0;
            }
            if (!compile.Any())
            {
                Console.Error.WriteLine("No source files are specified.");
                return 1;
            }

            if (outputDirectory == null)
            {
                Console.Error.WriteLine("The output directory must be specified.");
                return 2;
            }

            foreach(var prop in buildPropertyList) {
                var i = prop.IndexOf("=");

                if(i <= 0) {
                    continue;
                }

                var key = prop.Substring(0, i);
                var value = prop.Substring(i + 1);
                buildProperties[key] = value;
            }

            var generator = new CompilationGenerator
            {
                ProjectDirectory = projectDir,
                Compile = Sanitize(compile),
                ReferencePath = Sanitize(refs),
                PreprocessorSymbols = preprocessorSymbols,
                GeneratorAssemblySearchPaths = Sanitize(generatorSearchPaths),
                IntermediateOutputDirectory = outputDirectory,
                BuildProperties = buildProperties,
                AssemblyName = assemblyName
            };

            var progress = new Progress<Diagnostic>(OnDiagnosticProgress);

            try
            {
                generator.Generate(progress);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine($"{e.GetType().Name}: {e.Message}");
                Console.Error.WriteLine(e.ToString());
                return 3;
            }

            if (generatedCompileItemFile != null)
            {
                File.WriteAllLines(generatedCompileItemFile, generator.GeneratedFiles);
            }

            foreach (var file in generator.GeneratedFiles)
            {
                Logger.Info(file);
            }

            return 0;
        }

        private static void OnDiagnosticProgress(Diagnostic diagnostic)
        {
            Console.WriteLine(diagnostic.ToString());
        }

        private static IReadOnlyList<string> Sanitize(IReadOnlyList<string> inputs)
        {
            return inputs.Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => x.Trim()).ToArray();
        }
    }
}