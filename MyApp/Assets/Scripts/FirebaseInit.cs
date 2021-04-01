using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using Firebase.Analytics;
using UnityEngine;
using UnityEngine.Events;

public class FirebaseInit : MonoBehaviour
{
    public UnityEvent OnFireBaseInitialized = new UnityEvent();
    private static bool hasBeenInitialized = false;

    private void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Exception != null)
            {
                Debug.LogError($"Failed to initialize Firebase with {task.Exception}");
                return;
            }
            OnFireBaseInitialized.Invoke();
        });
        if (!hasBeenInitialized)
        {
            SaveSystem.Init();
            hasBeenInitialized = true;
        }
    }
}
