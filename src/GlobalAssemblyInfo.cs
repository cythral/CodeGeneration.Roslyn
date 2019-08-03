using System;
using System.Reflection;

[assembly: AssemblyVersion(CodeGeneration.Roslyn.ThisAssembly.Version)]

namespace CodeGeneration.Roslyn {
    partial class ThisAssembly {
        public const string Version = Git.BaseVersion.Major + "." + Git.BaseVersion.Minor + "." + Git.BaseVersion.Patch;
    }
}