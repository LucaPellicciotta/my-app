using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class NotificationSettingsScript : MonoBehaviour
{
    private NotificationScript.NotificationInfo notificationInfo;
    private UIManager uiManager;
    private NotificationScript notificationScript;

    [SerializeField] private TMP_Text valueText;
    [SerializeField] private TMP_Text header;

    private GameObject notificationScreen;

    private void Awake()
    {
        uiManager = GameObject.Find("UI").GetComponent<UIManager>();

        var objectsWithCounterScript = Resources.FindObjectsOfTypeAll<NotificationScript>();
        foreach (NotificationScript script in objectsWithCounterScript)
        {
            if (script.name == (gameObject.name + "_settings"))
            {
                notificationScript = script;
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
        header.text = notificationInfo.notificationName;
        valueText.text = notificationInfo.notificationName;
    }

    public void SetNotificationInfoInstance(NotificationScript.NotificationInfo _notificationInfo)
    {
        notificationInfo = _notificationInfo;
    }

    public void GoToMainScreen()
    {
        uiManager.LoadScreen(uiManager.screens[1]);
    }
}
