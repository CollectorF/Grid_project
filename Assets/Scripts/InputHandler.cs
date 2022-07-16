using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField rowsInput;
    [SerializeField]
    private TMP_InputField columnsInput;
    [SerializeField]
    private Button generateButton;
    [SerializeField]
    private Button mixButton;

    public delegate void InvalidInputEvent();
    public event InvalidInputEvent OnInvalidInput;

    internal void GetInput(out string userInputRows, out string userInputColumns)
    {
        userInputRows = rowsInput.text;
        userInputColumns = columnsInput.text;
    }

    internal int ParseInput(string input)
    {
        bool result = int.TryParse(input, out int number);
        if (result)
            return number;
        else
        {
            OnInvalidInput?.Invoke();
            return 0;
        }
    }

    internal void SetButtonsState(bool state)
    {
        rowsInput.interactable = state;
        columnsInput.interactable = state;
        generateButton.interactable = state;
        mixButton.interactable = state;
    }
}
