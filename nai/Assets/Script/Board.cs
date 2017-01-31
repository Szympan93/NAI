using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Board : MonoBehaviour
{
    public Texture2D MapTex;
    public Vector2 CostRange;
    public Field FieldPrefab;
    public Player PlayerPrefab;
    public Pathfinding.Map Map;
    public Player Player;
    public Field[,] Fields; 

    void Awake()
    {
        Map = new Map(MapTex.width, MapTex.height);
        Fields = new Field[Map.W,Map.H];
        for (int x = 0; x < Map.W; x++)
        {
            for (int y = 0; y < Map.H; y++)
            {
                Color color = MapTex.GetPixel(x, y);
                Tile tile;
                if (color.b != 1f)
                {
                    tile = new Tile(color.g * (CostRange.y-CostRange.x) + CostRange.x, true);
                }
                else
                {
                    tile = new Tile(0, false);
                }
                Map[x, y] = tile;
                Field field = Instantiate(FieldPrefab.gameObject, new Vector3(x - Map.W/2f, 0, y-Map.H/2f), Quaternion.identity, transform).GetComponent<Field>();
                field.X = x;
                field.Y = y;
                field.Tile = tile;
                Fields[x, y] = field;
                if (MapTex.GetPixel(x, y).b < 0.55f && MapTex.GetPixel(x, y).b > 0.45f)
                {
                    Player = Instantiate(PlayerPrefab.gameObject, new Vector3(x - Map.W / 2f, 0, y - Map.H / 2f), Quaternion.identity, transform).GetComponent<Player>();
                }
            }
        }
    }
}
