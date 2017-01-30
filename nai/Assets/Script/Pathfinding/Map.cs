using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
    public class Map
    {
        private Tile[,] _tiles;

        public int W { get { return _tiles.GetLength(0) - 2; } }
        public int H { get { return _tiles.GetLength(1) - 2; } }

        public Tile this[int x, int y]
        {
            get { return _tiles[x + 1, y + 1]; }
            set
            {
                value.X = x;
                value.Y = y;
                _tiles[x + 1, y + 1] = value;
            }
        }

        public Map(int w, int h)
        {
            _tiles = new Tile[w+2,h+2];

            // stworzenie niechodliwej ramki
            for (int i = 0; i < w + 2; i++)
            {
                _tiles[i, 0] = new Tile(0, false);
                _tiles[i, h + 1] = new Tile(0, false);
            }
            for (int i = 0; i < h + 2; i++)
            {
                _tiles[0, i] = new Tile(0, false);
                _tiles[w + 1, i] = new Tile(0, false);
            }
        }
    }
}
