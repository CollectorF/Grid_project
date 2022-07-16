using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InputHandler inputHandler;
    [SerializeField]
    private GridGenerator gridGenerator;
    [SerializeField]
    private GridShuffler gridShuffler;
    [SerializeField]
    private PopupHandler popupHandler;
    [SerializeField]
    private int rowsLimit = 15;
    [SerializeField]
    private int columnsLimit = 10;
    [SerializeField]
    private float nodesMoveDuration = 2;


    private string inputRows;
    private string inputColumns;

    private int rows;
    private int columns;

    private Node[,] currentGrid;
    private List<GameObject> gridNodes = new List<GameObject>();
    private Coroutine inputDisable;

    private void Awake()
    {
        gridGenerator.OnGridGenerated += SaveGrid;
        inputHandler.OnInvalidInput += DisplayPopupOnInvalidInput;
        inputHandler.SetButtonsState(true);
    }

    private void SaveGrid(Node[,] grid, List<GameObject> nodes)
    {
        currentGrid = grid;
        gridNodes = nodes;
    }

    public void OnGenerateClick()
    {
        SetGrid();
    }

    public void OnShuffleClick()
    {
        ShuffleGrid();
        if (inputDisable == null)
        {
            inputDisable = StartCoroutine(DisableInputCoroutine(nodesMoveDuration));
        }
    }

    private void SetGrid()
    {
        inputHandler.GetInput(out inputRows, out inputColumns);
        rows = inputHandler.ParseInput(inputRows);
        columns = inputHandler.ParseInput(inputColumns);
        if (CheckGridDimentions())
        {
            gridGenerator.SetupGrid(columns, rows);
        }
    }

    private void ShuffleGrid()
    {
        gridShuffler.ShuffleGrid(currentGrid, gridNodes, columns, rows, nodesMoveDuration);
    }

    private bool CheckGridDimentions()
    {
        if (rows > 0 && rows <= rowsLimit)
        {
            if (columns > 0 && columns <= columnsLimit) return true;
            else
            {
                DisplayPopupOnInvalidInput();
                return false;
            }
        }
        else
        {
            DisplayPopupOnInvalidInput();
            return false;
        }
    }

    private void DisplayPopupOnInvalidInput()
    {
        popupHandler.DisplayPopup(rowsLimit, columnsLimit);
    }

    private IEnumerator DisableInputCoroutine(float duration)
    {
        inputHandler.SetButtonsState(false);
        yield return new WaitForSeconds(duration);
        inputHandler.SetButtonsState(true);
        inputDisable = null;
    }
}
