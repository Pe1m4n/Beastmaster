using UnityEngine;

namespace Common.UnityExtensions
{
    public static class GameObjectExtensions
    {
        public static bool TryGetComponentInChildren<T>(this GameObject go, out T component) where T : Component
        {
            component = go.GetComponentInChildren<T>();
            return component != null;
        }
    }
}