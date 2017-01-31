using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;
using UnityEngine.EventSystems;

public class Field : MonoBehaviour, IPointerClickHandler
{
    [NonSerialized]
    public int X;
    [NonSerialized]
    public int Y;

    public Gradient Gradient;
    public Color WallColor;

    private SpriteRenderer _sr;
    private Board _board;
    private Tile _tile;

    public SpriteRenderer SR
    {
        get
        {
            if (_sr == null) _sr = GetComponentInChildren<SpriteRenderer>();
            return _sr;
        }
    }

    public Board Board
    {
        get
        {
            if (_board == null) _board = GetComponentInParent<Board>();
            return _board;
        }
    }

    public Tile Tile
    {
        get { return _tile; }
        set
        {
            _tile = value;
            _update();
        }
    }

    private void _update()
    {
        if (Tile.Walkable)
        {
            SR.color = Gradient.Evaluate((_tile.Cost - Board.CostRange.x)/(Board.CostRange.y - Board.CostRange.x));
        }
        else
        {
            SR.color = WallColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Board.Player.Path = Board.Map.FindPath(Board.Player.X, Board.Player.Y, X, Y);
    }
}
