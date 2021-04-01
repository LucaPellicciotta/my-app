using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class CounterScript : MonoBehaviour
{
    [System.Serializable]
    public class CounterInfo
    {
        public string name;
        public float counterValue;
        public CounterTypes counterType;
        public Vector2 position;

        public string[] unit = new string[2];


        public CounterInfo(string _name, float _counterValue, CounterTypes _counterType, Vector2 _position)
        {
            this.name = _name;
            this.counterValue = _counterValue;
            this.counterType = _counterType;
            this.position = _position;
        }

        public string[] GetCounterType()
        {
            switch (counterType)
            {
                case CounterTypes.Integer:
                    unit = new string[] { "", "" };
                    break;
                case CounterTypes.Time:
                    unit = new string[] { "h", "min." };
                    break;
            }

            return unit;
        }
    }


    public enum CounterTypes
    {
        Integer,
        Time
    }

    private CounterInfo counterInfo;
    private UIManager uiManager;

    [SerializeField] private TMP_Text counterText;

    private void Awake()
    {
        uiManager = GameObject.Find("UI").GetComponent<UIManager>();
    }

    private void Start()
    {
        gameObject.name = counterInfo.name;
        transform.localPosition = counterInfo.position;
    }

    private void Update()
    {
        UpdateCounterText();
    }

    public void SetCounterInfoInstance(CounterInfo _counterInfo)
    {
        counterInfo = _counterInfo;
    }

    public void UpdateCounterText()
    {
        gameObject.name = counterInfo.name;
        string newValueText = $"{counterInfo.name}: {counterInfo.counterValue}{counterInfo.unit[1]}";
        counterText.text = newValueText;
    }

    public void LoadCounterScreen()
    {
        for (int i = 0; i < uiManager.screens.Count; i++)
        {
            if (uiManager.screens[i].name == gameObject.name + "_screen")
                uiManager.LoadScreen(uiManager.screens[i]);
        }
    }
    
    #region Save/Load Functions
    public string GetName()
    {
        return counterInfo.name;
    }
    public void SetName(string newName)
    {
        counterInfo.name = newName;
        gameObject.name = newName;
    }

    public Vector2 GetPosition()
    {
        return counterInfo.position;
    }
    public void SetPosition(Vector2 newPosition)
    {
        counterInfo.position = newPosition;
        transform.localPosition = newPosition;
    }

    public float GetValue()
    {
        return counterInfo.counterValue;
    }
    public void SetValue(float newValue)
    {
        counterInfo.counterValue = newValue;
    }

    public CounterTypes GetCounterType()
    {
        return counterInfo.counterType;
    }
    public void SetCounterType(CounterTypes newCounterType)
    {
        counterInfo.counterType = newCounterType;
    }
    #endregion
}
