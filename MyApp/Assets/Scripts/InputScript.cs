using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class InputScript : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text inputFieldText;
    [SerializeField] private TMP_Text errorMessageText;

    private bool hasBeenFocused = false;

    private void Start()
    {
        SetErrorMessage("");
    }

    private void Update()
    { 
        if (inputField.isFocused)
            hasBeenFocused = true;

        if (hasBeenFocused)
            CheckForNullOrSpaces();

        if (inputField.isFocused && Input.GetKey(KeyCode.Return))
            uiManager.CreateCounter();
    }

    private void CheckForNullOrSpaces()
    {
        if (inputFieldText.text != null)
        {
            for (int i = 0; i < inputFieldText.text.Length - 1; i++)
            {
                if (!char.IsWhiteSpace(inputFieldText.text[i]))
                {
                    SetErrorMessage("");
                    return;
                }
            }
        }
        SetErrorMessage("No Name inserted!");
    }

    private void SetErrorMessage(string error)
    {
        errorMessageText.text = error;
    }
}
