using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private TMP_InputField emailField;
    [SerializeField] private TMP_InputField passwordField;
    [SerializeField] private TMP_InputField repeatPasswordField;

    private void Start()
    {
        emailField.onValueChanged.AddListener(HandleValueChanged);
        passwordField.onValueChanged.AddListener(HandleValueChanged);
        repeatPasswordField.onValueChanged.AddListener(HandleValueChanged);
    }

    private void HandleValueChanged(string _)
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (string.IsNullOrEmpty(emailField.text))
            Debug.Log("You should give email");// Give error or smthing

    }
}
