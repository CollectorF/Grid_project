using System;

[Serializable]
public class Node
{
    public char Value;
    public Point Index;

    public Node(char value, Point index)
    {
        Value = value;
        Index = index;
    }
}

