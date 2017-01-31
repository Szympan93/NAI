using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int X { get { return Mathf.RoundToInt((transform.position + _offset).x); } }
    public int Y { get { return Mathf.RoundToInt((transform.position + _offset).z); } }

    private List<Node> _path = new List<Node>();
    private float _pos;
    private Board _board;
    private Vector3 _offset;

    public List<Node> Path
    {
        get { return _path; }
        set
        {
            foreach (Node node in _path)
            {
                Board.Fields[node.X, node.Y].Tile = Board.Fields[node.X, node.Y].Tile;
            }

            _path = value;

            foreach (Node node in _path)
            {
                Board.Fields[node.X, node.Y].SR.color = Color.magenta;
            }
        }
    }

    public Board Board
    {
        get
        {
            if (_board == null)
            {
                _board = GetComponentInParent<Board>();
                _offset = new Vector3(Board.Map.W / 2f, 0, Board.Map.H / 2f);
                Debug.Log(_offset);
            }
            return _board;
        }
    }

    void Start()
    {
        Board b = Board;
    }

    void Update()
    {
        if (Path.Count < 2) return;

        _pos += Time.deltaTime / Board.Map[X, Y].Cost;
        if (_pos >= 1)
        {
            _pos -= 1;
            Board.Fields[_path[0].X, _path[0].Y].Tile = Board.Fields[_path[0].X, _path[0].Y].Tile;
            Path.RemoveAt(0);
        }
        if (Path.Count == 1)
        {
            transform.position = new Vector3(Path[0].X, 0, Path[0].Y) - _offset;
        }
        else
        {
            transform.position = Vector3.Lerp(new Vector3(Path[0].X, 0, Path[0].Y), new Vector3(Path[1].X, 0, Path[1].Y), _pos) - _offset;
        }
    }
}
