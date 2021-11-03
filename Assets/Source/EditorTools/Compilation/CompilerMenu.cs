using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using UnityEditor;
using UnityEngine;

namespace EditorTools.Compilation
{
    public class CompilerMenu
    {
        [MenuItem("Assets/CompileDll")]
        public static void CompileDll()
        {
            var compilationData = Selection.activeObject as RoslynCompileData;

            if (compilationData == null)
                return;

            var sourceFiles = new List<string>();
            foreach (var asmDef in compilationData.UnityAssemblyReferences)
            {
                var asmPath = Path.GetFullPath(AssetDatabase.GetAssetPath(asmDef));
                var dirPath = asmPath.Remove(asmPath.LastIndexOf("\\"));
                var directory = new DirectoryInfo(dirPath); 
                sourceFiles.AddRange(directory.EnumerateFiles("*.cs", SearchOption.AllDirectories)                
                    .Select(a => a.FullName));
            }

            var trees = new List<SyntaxTree>();
            foreach (var file in sourceFiles)
            {
                var code = File.ReadAllText(file);
                var tree = CSharpSyntaxTree.ParseText(code, path:file);
                trees.Add(tree);
            }
          
            MetadataReference mscorlib =
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location);
            MetadataReference codeAnalysis =
                MetadataReference.CreateFromFile(typeof(SyntaxTree).Assembly.Location);
            MetadataReference csharpCodeAnalysis =
                MetadataReference.CreateFromFile(typeof(CSharpSyntaxTree).Assembly.Location);
            MetadataReference systemDiagnostics =
                MetadataReference.CreateFromFile(typeof(System.Diagnostics.Debug).Assembly.Location);
     
            MetadataReference[] references = { mscorlib, codeAnalysis, csharpCodeAnalysis, systemDiagnostics };

            var outputName = $"{compilationData.OutputNameWithoutExtension}.dll";
            var compilation = CSharpCompilation.Create(outputName,
                trees,
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: compilationData.Unsafe));
            var result = compilation.Emit(Path.Combine(compilationData.OutputDir, outputName));
            
            Debug.LogError($"Compilation of {outputName} was {(result.Success ? "Successful" : "Failed")}");
            foreach (var diagnostic in result.Diagnostics)
            {
                Debug.LogError($"{diagnostic.GetMessage()} in {diagnostic.Location.SourceTree.FilePath}");
            }
            
            if (!result.Success)
                File.Delete($"{Path.GetFullPath(compilationData.OutputDir)}{outputName}");
        }
        
        [MenuItem("Assets/CompileDll", true)]
        public static bool CompileDllValidation()
        {
            return Selection.activeObject is RoslynCompileData;
        }
    }
}