using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Tile
    {
        public int X;
        public int Y;
        public float Cost;
        public bool Walkable;

        public Tile(float cost, bool walkable)
        {
            Cost = cost;
            Walkable = walkable;
        }
    }
}
