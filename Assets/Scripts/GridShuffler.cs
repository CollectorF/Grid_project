using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridShuffler : MonoBehaviour
{
    private Node[,] currentGrid;
    private Node[,] shuffledGrid;

    internal void ShuffleGrid(Node[,] grid, List<GameObject> nodes, int columns, int rows, float duration)
    {
        currentGrid = grid;
        shuffledGrid = currentGrid;
        ShuffleArray(shuffledGrid);

        GameObject currentObject = null;
        RectTransform rect = null;
        Node newNode = null;
        foreach (var currentNode in currentGrid)
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    if (currentNode.Index == shuffledGrid[x, y].Index)
                    {
                        newNode = shuffledGrid[x, y];
                        currentObject = nodes.Find(item => item.name == $"{ x },{ y }");
                        rect = currentObject.GetComponent<RectTransform>();
                        break;
                    }
                }
            }
            StartCoroutine(MoveNode(rect, newNode.LocalPos, duration));
        }
    }

    private void ShuffleArray(Node[,] array)
    {
        System.Random random = new System.Random();
        int lengthRow = array.GetLength(1);

        for (int i = array.Length - 1; i > 0; i--)
        {
            int i0 = i / lengthRow;
            int i1 = i % lengthRow;

            int j = random.Next(i + 1);
            int j0 = j / lengthRow;
            int j1 = j % lengthRow;

            Node temp = array[i0, i1];
            array[i0, i1] = array[j0, j1];
            array[j0, j1] = temp;
        }
    }

    private IEnumerator MoveNode(RectTransform rect, Vector2 endPos, float moveDuration)
    {
        Vector2 startPos = rect.localPosition;
        float timeElapsed = 0;
        while (timeElapsed < moveDuration)
        {
            rect.localPosition = Vector2.Lerp(startPos, endPos, timeElapsed / moveDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        rect.localPosition = endPos;
    }
}
