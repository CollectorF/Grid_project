using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField rowsInput;
    [SerializeField]
    private TMP_InputField columnsInput;

    private string userInputRows;
    private string userInputColumns;

    public delegate void InputEvent(string str1, string str2);

    public event InputEvent OnInput;

    public void GetUserInput()
    {
        userInputRows = rowsInput.text;
        userInputColumns = columnsInput.text;
        OnInput?.Invoke(userInputRows, userInputColumns);
    }
}
