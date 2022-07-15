using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GridHandler : MonoBehaviour
{
    [SerializeField]
    private string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    [SerializeField]
    private GameObject nodePrefab;
    [SerializeField]
    private RectTransform gameGrid;

    private Node[,] grid;
    private int rows;
    private int columns;
    private GameObject scaledPrefab;

    private void Start()
    {
        rows = 20;
        columns = 4;

        SetPrefabSize();
        GenerateGrid();
        InstantiateGrid();
    }

    private void GenerateGrid()
    {
        grid = new Node[columns, rows];
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                grid[x, y] = new Node(GetRandomChar(), new Point(x, y));
            }
        }
    }

    private void InstantiateGrid()
    {
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < columns; x++)
            {
                GameObject currentNode = Instantiate(nodePrefab, gameGrid);
                RectTransform rect = currentNode.GetComponent<RectTransform>();
                rect.anchoredPosition = new 
                    (
                    rect.sizeDelta.x / 2 + (rect.sizeDelta.x * x),
                    -rect.sizeDelta.y / 2 - (rect.sizeDelta.y * y)
                    );
                TextMeshProUGUI textField = currentNode.GetComponentInChildren<TextMeshProUGUI>();
                textField.text = grid[x, y].Value.ToString();
            }
        }
    }

    private void SetPrefabSize()
    {
        scaledPrefab = nodePrefab;
        RectTransform prefabTransform = nodePrefab.GetComponent<RectTransform>();
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
