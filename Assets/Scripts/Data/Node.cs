using UnityEngine;

public class Node
{
    public char Value;
    public Point Index;
    public Vector2 LocalPos;

    public Node(char value, Point index)
    {
        Value = value;
        Index = index;
        LocalPos = Vector2.zero;
    }
}

