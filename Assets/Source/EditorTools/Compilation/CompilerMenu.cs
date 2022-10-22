using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditorInternal;
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

            var excludedAsmDefs = AssetDatabase.FindAssets("t:AssemblyDefinitionAsset")
                .Select(AssetDatabase.GUIDToAssetPath).Select(AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>)
                .Where(a => !compilationData.UnityAssemblyReferences.Contains(a)).Select(AssetDatabase.GetAssetPath).Select(Path.GetDirectoryName).Select(p => p.Replace("\\", "/")).ToArray();
            
            foreach (var asmDef in compilationData.UnityAssemblyReferences)
            {
                var asmPath = Path.GetFullPath(AssetDatabase.GetAssetPath(asmDef));
                var dirPath = asmPath.Remove(asmPath.LastIndexOf("\\"));
                var directory = new DirectoryInfo(dirPath);
                sourceFiles.AddRange(directory.EnumerateFiles("*.cs", SearchOption.AllDirectories)                
                    .Select(a => a.FullName.Replace("\\", "/")).Where(path => excludedAsmDefs.All(p => !path.Contains(p))));
            }

            var trees = new List<SyntaxTree>();
            foreach (var file in sourceFiles)
            {
                var code = File.ReadAllText(file);
                var tree = CSharpSyntaxTree.ParseText(code, path:file);
                trees.Add(tree);
            }
     
            var references = new []{ 
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(SyntaxTree).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(CSharpSyntaxTree).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Diagnostics.Debug).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(Assembly.Load("netstandard, Version=2.0.0.0").Location),
                //MetadataReference.CreateFromFile(typeof(JsonConvert).Assembly.Location),
            };
            
            var outputName = $"{compilationData.OutputNameWithoutExtension}";
            var compilation = CSharpCompilation.Create(outputName,
                trees,
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, allowUnsafe: compilationData.Unsafe));
            var result = compilation.Emit(Path.Combine(compilationData.OutputDir, $"{outputName}.dll"));
            
            if (!result.Success)
                File.Delete($"{Path.GetFullPath(compilationData.OutputDir)}{outputName}");
            
            Debug.LogError($"Compilation of {outputName}.dll was {(result.Success ? "Successful" : "Failed")}");
            foreach (var diagnostic in result.Diagnostics)
            {
                Debug.LogError($"{diagnostic.GetMessage()} in {diagnostic.Location.SourceTree.FilePath}");
            }
        }
        
        [MenuItem("Assets/CompileDll", true)]
        public static bool CompileDllValidation()
        {
            return Selection.activeObject is RoslynCompileData;
        }
    }
}