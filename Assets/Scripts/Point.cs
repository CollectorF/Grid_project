using System;
using UnityEngine;

[Serializable]
public class Point
{
    public int X;
    public int Y;

    public Point(int newX, int newY)
    {
        X = newX;
        Y = newY;
    }

    public static Point FromVector(Vector2 vector)
    {
        return new Point((int)vector.x, (int)vector.y);
    }
}
