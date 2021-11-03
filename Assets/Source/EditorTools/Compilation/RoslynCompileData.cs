using UnityEditorInternal;
using UnityEngine;

namespace EditorTools.Compilation
{
    public class RoslynCompileData : ScriptableObject
    {
        public string OutputNameWithoutExtension;
        public string OutputDir;
        public AssemblyDefinitionAsset[] UnityAssemblyReferences;
        public bool Unsafe;
    }
}