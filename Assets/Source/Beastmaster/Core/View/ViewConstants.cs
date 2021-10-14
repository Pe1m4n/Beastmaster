using System.Collections.Generic;
using Beastmaster.Core.Primitives;
using UnityEngine;

namespace Beastmaster.Core.View
{
    public class ViewConstants
    {
        //TODO: equality comparer?
        public static readonly Dictionary<Direction, Quaternion> Directions = new Dictionary<Direction, Quaternion>()
        {
            { Direction.Top, Quaternion.identity},
            { Direction.Right , Quaternion.Euler(0, 90, 0)},
            { Direction.Bottom , Quaternion.Euler(0, 180, 0)},
            { Direction.Left , Quaternion.Euler(0, 270, 0)},
            
            { Direction.TopRight , Quaternion.Euler(0, 45, 0)},
            { Direction.BottomRight , Quaternion.Euler(0, 135, 0)},
            { Direction.BottomLeft , Quaternion.Euler(0, 225, 0)},
            { Direction.TopLeft , Quaternion.Euler(0, 315, 0)},
        };
    }
}