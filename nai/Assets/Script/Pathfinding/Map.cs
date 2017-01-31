using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Pathfinding
{
    public class Map
    {
        private static readonly float Sqrt2;

        static Map()
        {
            Sqrt2 = (float)Math.Sqrt(2);
        }

        private Tile[,] _tiles;

        public int W { get { return _tiles.GetLength(0) - 2; } }
        public int H { get { return _tiles.GetLength(1) - 2; } }
        public Tile this[int x, int y]
        {
            get { return _tiles[x + 1, y + 1]; }
            set { _tiles[x + 1, y + 1] = value; }
        }

        public Map(int w, int h)
        {
            _tiles = new Tile[w+2,h+2];
            for (int i = 0; i < w + 2; i++)
            {
                _tiles[i, 0] = new Tile(0, false);
                _tiles[i, h + 1] = new Tile(0, false);
            }
            for (int i = 0; i < h + 2; i++)
            {
                _tiles[0, i] = new Tile(0, false);
                _tiles[w+1, i] = new Tile(0, false);
            }
        }

        public List<Node> FindPath(int sx, int sy, int dx, int dy)
        {
            List<Node> nodes = new List<Node>();
            Node node = _findPath(sx+1, sy+1, dx+1, dy+1);
            while (node != null)
            {
                node.X -= 1;
                node.Y -= 1;
                nodes.Insert(0, node);
                node = node.Parent;
            }
            return nodes;
        }

        private float[,] _createDistanceMap(int tx, int ty)
        {
            float[,] map = new float[_tiles.GetLength(0), _tiles.GetLength(1)];

            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    int dx = Math.Abs(tx - x);
                    int dy = Math.Abs(ty - y);
                    int dMin;
                    int dDif;

                    if (dx > dy)
                    {
                        dMin = dy;
                        dDif = dx - dy;
                    }
                    else
                    {
                        dMin = dx;
                        dDif = dy - dx;
                    }

                    map[x, y] = dMin*Sqrt2 + dDif;
                }
            }
            return map;
        }

        private Node _findPath(int sx, int sy, int dx, int dy)
        {
            float[,] distances = _createDistanceMap(dx, dy);
            Node[,] nodes = new Node[_tiles.GetLength(0),_tiles.GetLength(1)];
            nodes[sx, sy] = new Node(sx, sy, null, 0);
            List<Node> openNodes = new List<Node>(new []{nodes[sx, sy]});
            
            while (openNodes.Count > 0)
            {
                Node cNode = null;
                float bestDistance = float.MaxValue;
                foreach (Node node in openNodes)
                {
                    float estimatedCost = node.Cost + distances[node.X, node.Y];
                    if (estimatedCost < bestDistance)
                    {
                        bestDistance = estimatedCost;
                        cNode = node;
                    }
                }
                cNode.State = NodeState.Closed;
                openNodes.Remove(cNode);

                if (cNode.X == dx && cNode.Y == dy) return cNode;

                Node tNode = _addNode(cNode.X + 1, cNode.Y, 1, ref cNode, ref nodes);
                if (tNode != null) openNodes.Add(tNode);
                tNode = _addNode(cNode.X - 1, cNode.Y, 1, ref cNode, ref nodes);
                if (tNode != null) openNodes.Add(tNode);
                tNode = _addNode(cNode.X, cNode.Y + 1, 1, ref cNode, ref nodes);
                if (tNode != null) openNodes.Add(tNode);
                tNode = _addNode(cNode.X, cNode.Y - 1, 1, ref cNode, ref nodes);
                if (tNode != null) openNodes.Add(tNode);

                tNode = _addNode(cNode.X + 1, cNode.Y + 1, Sqrt2, ref cNode, ref nodes);
                if (tNode != null) openNodes.Add(tNode);
                tNode = _addNode(cNode.X - 1, cNode.Y - 1, Sqrt2, ref cNode, ref nodes);
                if (tNode != null) openNodes.Add(tNode);
                tNode = _addNode(cNode.X - 1, cNode.Y + 1, Sqrt2, ref cNode, ref nodes);
                if (tNode != null) openNodes.Add(tNode);
                tNode = _addNode(cNode.X + 1, cNode.Y - 1, Sqrt2, ref cNode, ref nodes);
                if (tNode != null) openNodes.Add(tNode);
            }
            return null;
        }

        private Node _addNode(int tx, int ty, float distance, ref Node cNode, ref Node[,] nodes)
        {
            Node tNode = nodes[tx, ty];
            if (tNode == null)
            {
                if (_tiles[tx, ty].Walkable)
                {
                    tNode = new Node(tx, ty, cNode, _tiles[tx, ty].Cost);
                    nodes[tx, ty] = tNode;
                    return tNode;
                }
            }
            else if (tNode.State == NodeState.Opened)
            {
                float estimatedCost = cNode.Cost + _tiles[tx, ty].Cost * distance;
                if (tNode.Cost > estimatedCost)
                {
                    tNode.Parent = cNode;
                    tNode.Cost = estimatedCost;
                }
            }
            return null;
        }
    }
}