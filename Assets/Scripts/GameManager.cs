using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private InputHandler inputHandler;
    [SerializeField]
    private GridHandler gridHandler;

    private void Start()
    {
        inputHandler.OnInput += SetGrid;
    }

    private void SetGrid(string a, string b)
    {
        gridHandler.SetupGrid(int.Parse(a), int.Parse(b));
    }
}
