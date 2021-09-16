using Beastmaster.Core.Primitives;
using UnityEngine;

namespace Beastmaster.Core.View
{
    public class TileView : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        public Coordinates Coordinates { get; private set; }
        
        public void Init(Coordinates coordinates)
        {
            Coordinates = coordinates;
        }

        public void SetColor(Color color)
        {
            _renderer.material.color = color;
        }
    }
}