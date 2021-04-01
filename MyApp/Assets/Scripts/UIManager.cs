using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class UIManager : MonoBehaviour
{
    [SerializeField] private  GameObject counterPrefab;
    [SerializeField] private  GameObject counterScreenPrefab;

    public List<GameObject> screens = new List<GameObject>();
    public static List<GameObject> counters = new List<GameObject>();
    public static List<CounterScript.CounterInfo> counterInfos = new List<CounterScript.CounterInfo>();
    public static List<CounterScript.CounterInfo> newLoadedCounterInfos = new List<CounterScript.CounterInfo>();

    [SerializeField] private TMP_InputField counterInputfield;
    [SerializeField] private TMP_Dropdown counterDropDown;

    private Vector2 newPosition = new Vector2(-15, 60);
    private float positionSlack = 60f;


    [SerializeField] private GameObject notificationPrefab;
    [SerializeField] private GameObject notificationSettingsPrefab;

    public static List<GameObject> notifications = new List<GameObject>();
    public static List<NotificationScript.NotificationInfo> notificationInfos = new List<NotificationScript.NotificationInfo>();
    public List<GameObject> notificationSettings = new List<GameObject>();

    [SerializeField] private TMP_InputField notificationNameInputfield;
    [SerializeField] private TMP_InputField notificationTitleInputfield;
    [SerializeField] private TMP_InputField notificationTextInputfield;
    [SerializeField] private TMP_Text notificationHour;
    [SerializeField] private TMP_Text notificationMinute;

    private Vector2 newNotificationPosition = new Vector2(-15, 60);

    private void Start()
    {
        LoadScreen(screens[0]);
    }

    public void DestroyAllCounters()
    {
        foreach (GameObject counter in counters)
            Destroy(counter);
    }

    public void CreateCounter()
    {
        if (counterInfos.Count != 0)
            newPosition.y = counterInfos.Min(counterinfo => counterinfo.position.y) - positionSlack;
        else
            newPosition.y = 80;

        var newCounter = Instantiate(counterPrefab, new Vector2(0, 0), Quaternion.identity, screens[0].transform);
        CounterScript.CounterInfo newCounterInfo = new CounterScript.CounterInfo(counterInputfield.text, 0, (CounterScript.CounterTypes)Enum.Parse(typeof(CounterScript.CounterTypes), counterDropDown.options[counterDropDown.value].text.ToString(), false), newPosition);
        newCounter.GetComponent<CounterScript>().SetCounterInfoInstance(newCounterInfo);
        counters.Add(newCounter);
        counterInfos.Add(newCounterInfo);

        var newCounterScreen = Instantiate(counterScreenPrefab, screens[0].transform.parent);
        newCounterScreen.name = counterInputfield.text + "_screen";
        newCounterScreen.GetComponent<CounterScreenScript>().SetCounterInfoInstance(newCounterInfo);
        screens.Add(newCounterScreen);

        counterInputfield.transform.GetComponentInParent<TMP_InputField>().Select();
        counterInputfield.text = "";
        newPosition.y -= 60f;

        LoadScreen(newCounterScreen);
    }

    public void CreateCounterFromLoad(string newName, float newValue, CounterScript.CounterTypes newCounterType, Vector2 newPosition)
    {
        var newCounter = Instantiate(counterPrefab, new Vector2(0, 0), Quaternion.identity, screens[0].transform);
        CounterScript.CounterInfo newCounterInfo = new CounterScript.CounterInfo(newName, newValue, newCounterType, newPosition);
        newCounter.GetComponent<CounterScript>().SetCounterInfoInstance(newCounterInfo);
        counters.Add(newCounter);
        counterInfos.Add(newCounterInfo);
    }

    public void CreateCounterFromLoadedCounterInfo(CounterScript.CounterInfo counterInfo)
    {
        var newCounter = Instantiate(counterPrefab, new Vector2(0, 0), Quaternion.identity, screens[0].transform);
        newCounter.gameObject.name = counterInfo.name;
        CounterScript.CounterInfo newCounterInfo = new CounterScript.CounterInfo(counterInfo.name, counterInfo.counterValue, counterInfo.counterType, counterInfo.position);
        newCounter.GetComponent<CounterScript>().SetCounterInfoInstance(newCounterInfo);
        newCounter.gameObject.SetActive(true);
        counters.Add(newCounter);
        counterInfos.Add(newCounterInfo);

        var newCounterScreen = Instantiate(counterScreenPrefab, screens[0].transform.parent);
        newCounterScreen.gameObject.name = counterInfo.name + "_screen";
        newCounterScreen.GetComponent<CounterScreenScript>().SetCounterInfoInstance(newCounterInfo);
        screens.Add(newCounterScreen);
        newCounter.SetActive(false);

        LoadScreen(screens[0]);
    }

    public void CreateNotification()
    {
        if (notificationInfos.Count != 0)
            newNotificationPosition.y = notificationInfos.Min(counterInfo => counterInfo.position.y) - positionSlack;
        else
            newNotificationPosition.y = 80;

        var newNotification = Instantiate(notificationPrefab, new Vector2(0, 0), Quaternion.identity, screens[1].transform);
        newNotification.name = notificationNameInputfield.text.ToString();
        DateTime fireTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(notificationHour.text), int.Parse(notificationMinute.text), 0);
        NotificationScript.NotificationInfo newNotificationInfo = new NotificationScript.NotificationInfo(notificationNameInputfield.text.ToString(), notificationTitleInputfield.text.ToString(), notificationTextInputfield.text.ToString(), fireTime, newNotificationPosition);
        newNotification.GetComponent<NotificationScript>().SetNotificaionInfoInstance(newNotificationInfo);
        notifications.Add(newNotification);
        notificationInfos.Add(newNotificationInfo);

        var newNotificationSettings = Instantiate(notificationSettingsPrefab, screens[0].transform.parent);
        newNotificationSettings.name = notificationNameInputfield.text.ToString() + "_settings";
        newNotificationSettings.GetComponent<NotificationSettingsScript>().SetNotificationInfoInstance(newNotificationInfo);
        notificationSettings.Add(newNotificationSettings);

        notificationNameInputfield.transform.GetComponentInParent<TMP_InputField>().Select();
        notificationNameInputfield.text = "";
        notificationTitleInputfield.transform.GetComponentInParent<TMP_InputField>().Select();
        notificationTitleInputfield.text = "";
        notificationTextInputfield.transform.GetComponentInParent<TMP_InputField>().Select();
        notificationTextInputfield.text = "";

        newPosition.y -= 60f;

        LoadScreen(screens[1]);
    }

    public void LoadScreen(GameObject toLoadScreen)
    {
        foreach (GameObject screen in screens)
            screen.SetActive(false);
        foreach (GameObject screen in notificationSettings)
            screen.SetActive(false);

        toLoadScreen.SetActive(true);
        foreach (Transform child in toLoadScreen.transform)
            child.gameObject.SetActive(true);
    }

}
