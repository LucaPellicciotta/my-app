using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class CounterScreenScript : MonoBehaviour
{
    private CounterScript.CounterInfo counterInfo;
    private UIManager uiManager;
    private CounterScript counterScript;

    [SerializeField] private TMP_Text valueText;
    [SerializeField] private TMP_Text header;

    private void Awake()
    {
        uiManager = GameObject.Find("UI").GetComponent<UIManager>();

        var objectsWithCounterScript = Resources.FindObjectsOfTypeAll<CounterScript>();
        foreach (CounterScript script in objectsWithCounterScript)
        {
            if (script.name == (gameObject.name + "_screen"))
            {
                counterScript = script;
                break;
            }
        }
    }

    private void Update()
    {
        UpdateValueText();
    }

    public void UpdateValueText()
    {
        header.text = counterInfo.name;
        valueText.text = $"{counterInfo.name}: {counterInfo.counterValue}{counterInfo.GetCounterType()[1]}";
    }

    public void SetCounterInfoInstance(CounterScript.CounterInfo _counterInfo)
    {
        counterInfo = _counterInfo;
    }

    public void GoToMainScreen()
    {
        uiManager.LoadScreen(uiManager.screens[0]); 
    }

    public void ChangeValue(bool higher)
    {
        if (higher)
            counterInfo.counterValue++;
        else
            counterInfo.counterValue--;

    }
}
