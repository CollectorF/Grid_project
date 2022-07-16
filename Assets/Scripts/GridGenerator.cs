using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField]
    private string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    [SerializeField]
    private GameObject nodePrefab;
    [SerializeField]
    private RectTransform gameGrid;

    private Node[,] initialGrid;
    private GameObject scaledPrefab;
    private List<GameObject> gridNodes = new List<GameObject>();

    public delegate void GridGeneratedEvent(Node[,] grid, List<GameObject> nodes);
    public event GridGeneratedEvent OnGridGenerated;


    internal void SetupGrid(int columns, int rows)
    {
        DestroyGrid();
        SetPrefabSize(columns, rows);
        GenerateGrid(columns, rows);
        InstantiateGrid(columns, rows);
        OnGridGenerated?.Invoke(initialGrid, gridNodes);
    }


    private void GenerateGrid(int columns, int rows)
    {
        initialGrid = new Node[columns, rows];
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                initialGrid[x, y] = new Node(GetRandomChar(), new Point(x, y));
            }
        }
    }

    private void InstantiateGrid(int columns, int rows)
    {
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                GameObject currentNode = Instantiate(scaledPrefab, gameGrid);
                currentNode.name = $"{ x },{ y }";
                gridNodes.Add(currentNode);
                RectTransform rect = currentNode.GetComponent<RectTransform>();
                rect.anchoredPosition = new 
                    (
                    rect.sizeDelta.x / 2 + (rect.sizeDelta.x * x),
                    -rect.sizeDelta.y / 2 - (rect.sizeDelta.y * y)
                    );
                TextMeshProUGUI textField = currentNode.GetComponentInChildren<TextMeshProUGUI>();
                textField.text = initialGrid[x, y].Value.ToString();
                initialGrid[x, y].LocalPos = rect.localPosition;
            }
        }
    }


    private void DestroyGrid()
    {
        if (gridNodes.Count != 0)
        {
            foreach (var item in gridNodes)
            {
                Destroy(item.gameObject);
            }
            gridNodes.Clear();
        }
    }


    private void SetPrefabSize(int columns, int rows)
    {
        scaledPrefab = nodePrefab;
        RectTransform prefabTransform = scaledPrefab.GetComponent<RectTransform>();
        float dimention;
        if (columns >= rows)
        {
            dimention = gameGrid.rect.width / columns;
        }
        else
        {
            dimention = gameGrid.rect.height / rows;
        }
        prefabTransform.sizeDelta = new Vector2(dimention, dimention);
    }


    public char GetRandomChar()
    {
        System.Random random = new System.Random();
        char character = chars[random.Next(chars.Length)];
        return character;
    }
}
