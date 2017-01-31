using System;
using System.Collections;
using System.Collections.Generic;
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
    }
}