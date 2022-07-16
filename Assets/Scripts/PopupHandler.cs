using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PopupHandler : MonoBehaviour
{
    [SerializeField]
    private float displayDuration = 2;

    private TextMeshProUGUI textField;
    private Coroutine displayPopupCoroutine;

    private void Start()
    {
        textField = GetComponentInChildren<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    internal void DisplayPopup(int rows, int columns)
    {
        gameObject.SetActive(true);
        textField.text = $"Некорректный ввод!\nТолько цифры, не более {rows} рядов и {columns} столбцов";
        if (displayPopupCoroutine == null)
        {
            displayPopupCoroutine = StartCoroutine(PopupCoroutine(displayDuration));
        }
    }

    private IEnumerator PopupCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
        displayPopupCoroutine = null;
    }
}
