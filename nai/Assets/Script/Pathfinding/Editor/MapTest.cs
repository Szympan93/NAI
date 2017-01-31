using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using System.Collections.Generic;

namespace Pathfinding.Test
{
    public class MapTest
    {
        [Test]
        public void SimpleTest()
        {
            Map map = new Map(5, 5);
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    map[x, y] = new Tile(1, true);
                }
            }
            List<Node> nodes = map.FindPath(0, 0, 4, 4);
            Assert.IsTrue(nodes.Count == 5);
            Assert.IsTrue(nodes[0].X == 0 && nodes[0].Y == 0);
            Assert.IsTrue(nodes[2].X == 2 && nodes[2].Y == 2);
            Assert.IsTrue(nodes[4].X == 4 && nodes[4].Y == 4);
        }

        [Test]
        public void ObstacleTest()
        {
            Map map = new Map(5, 5);
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    map[x, y] = new Tile(1, true);
                }
            }

            for (int i = 1; i < 4; i++)
            {
                map[i, 2] = new Tile(0, false);
            }

            List<Node> nodes = map.FindPath(0, 0, 4, 4);
            Assert.IsTrue(nodes.Count == 7);
        }

        [Test]
        public void UnpassableTest()
        {
            Map map = new Map(5, 5);
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    map[x, y] = new Tile(1, true);
                }
            }

            for (int i = 0; i < 5; i++)
            {
                map[i, 2] = new Tile(0, false);
            }

            List<Node> nodes = map.FindPath(0, 0, 4, 4);
            Assert.IsTrue(nodes.Count == 0);
        }

        [Test]
        public void PassableVariableCostTest()
        {
            Map map = new Map(5, 5);
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    map[x, y] = new Tile(1, true);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                map[i, 1] = new Tile(2, true);
            }

            List<Node> nodes = map.FindPath(0, 0, 4, 4);
            Assert.IsTrue(nodes.Count == 5);
        }

        [Test]
        public void UnpassableVariableCostTest()
        {
            Map map = new Map(5, 5);
            for (int x = 0; x < 5; x++)
            {
                for (int y = 0; y < 5; y++)
                {
                    map[x, y] = new Tile(1, true);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                map[i, 1] = new Tile(20, true);
            }

            List<Node> nodes = map.FindPath(0, 0, 4, 4);
            Assert.IsTrue(nodes.Count == 8);
        }
    }
}
