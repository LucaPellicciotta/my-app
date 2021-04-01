using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationScript : MonoBehaviour
{
    [System.Serializable]
    public class NotificationInfo
    {
        public string notificationName;
        public string notificationTitle;
        public string notificationText;
        public System.DateTime fireTime;
        public Vector2 position;

        public NotificationInfo(string _notificationName, string _notificationTitle, string _notificationText, System.DateTime _fireTime, Vector2 _position)
        {
            this.notificationName = _notificationName;
            this.notificationTitle = _notificationTitle;
            this.notificationText = _notificationText;
            this.fireTime = _fireTime;
            this.position = _position;
        }
    }

    private NotificationInfo notificationInfo;
    private UIManager uiManager;

    [SerializeField] private TMP_Text counterText;

    private void Awake()
    {
        uiManager = GameObject.Find("UI").GetComponent<UIManager>();
    }

    private void Start()
    {
        gameObject.name = notificationInfo.notificationName;
        transform.localPosition = notificationInfo.position;
    }

    private void Update()
    {
        //UpdateNotificationText();
    }

    public void SetNotificaionInfoInstance(NotificationInfo _notificationInfo)
    {
        notificationInfo = _notificationInfo;
    }

    public void UpdateNotificationText()
    {
        gameObject.name = notificationInfo.notificationName;
        string newNameText = notificationInfo.notificationName;
        counterText.text = newNameText;
    }

    public void LoadNotificationSettings()
    {
        for (int i = 0; i < uiManager.notificationSettings.Count; i++)
        {
            if (uiManager.notificationSettings[i].name == gameObject.name + "_settings")
            {
                uiManager.LoadScreen(uiManager.notificationSettings[i]);
            }
        }
    }
}
