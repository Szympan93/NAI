using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Node
    {
        public int X;
        public int Y;
        public NodeState State;
        public float Cost;
        public Node Parent;

        public Node(int x, int y, Node parent, float cost)
        {
            X = x;
            Y = y;
            Parent = parent;
            Cost = cost;
            if (Parent != null) Cost += Parent.Cost;
            State = NodeState.Opened;
        }
    }
}