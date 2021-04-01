using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Extensions;
using Firebase.Unity;
using Firebase.Database;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    private const string PLAYER_KEY = "PLAYER_KEY";

    private FirebaseDatabase database;
    private UIManager uiManager;

    private void Awake()
    {
        uiManager = GameObject.Find("UI").GetComponent<UIManager>();

        database = FirebaseDatabase.DefaultInstance;
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "SportScene")
            StartCoroutine(ReadFirebaseData());

        SaveSystem.CreateLocalDataFromFirebase();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveSystem.Save();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(ReadFirebaseData());
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            SaveSystem.DeleteAllData();
        }
    }

    private void LoadFromJson()
    {
        string saveString = SaveSystem.ReadDataLocally();

        if (saveString != null)
        {
            string[] splitSaveStrings = saveString.Split(new string[] { "\n" }, StringSplitOptions.None);
            for (int i = 0; i < splitSaveStrings.Length - 1; i++)
            {
                uiManager.CreateCounterFromLoadedCounterInfo(JsonUtility.FromJson<CounterScript.CounterInfo>(splitSaveStrings[i]));
            }
        }
    }

    private IEnumerator ReadFirebaseData() // New One: to be tested
    {
        List<CounterScript.CounterInfo> newCounters = new List<CounterScript.CounterInfo>();
        List<Task<CounterScript.CounterInfo>> loadTasks = new List<Task<CounterScript.CounterInfo>>();

        for (int i = 0; i < SaveSystem.GetChildCount(); i++)
        {
            var loadDataTask = SaveSystem.ReadDataOnline(SaveSystem.COUNTER_SAVE_PATH + i.ToString());
            loadTasks.Add(loadDataTask);
        }

        foreach (Task task in loadTasks)
        {
            yield return new WaitUntil(predicate: () => task.IsCompleted);
        }

        foreach (Task<CounterScript.CounterInfo> counterInfo in loadTasks)
        {
            uiManager.CreateCounterFromLoadedCounterInfo(counterInfo.Result);
        }
    }

    //private IEnumerator Load()
    //{
    //    // 1. get all values
    //    List<CounterScript.CounterInfo> newcounters = new List<CounterScript.CounterInfo>();
    //    List<Task<CounterScript.CounterInfo>> loadtasks = new List<Task<CounterScript.CounterInfo>>();

    //    for (int i = 0; i < SaveSystem.GetChildCount(); i++)
    //    {
    //        var loaddatatask = SaveSystem.ReadDataOnline("counters/" + i.ToString());
    //        loadtasks.Add(loaddatatask);
    //    }

    //    foreach (Task<CounterScript.CounterInfo> task in loadtasks)
    //    {
    //        yield return new WaitUntil(predicate: () => task.IsCompleted);
    //    }
    //    // 2. update ui
    //    uiManager.DestroyAllCounters();
    //    foreach (Task<CounterScript.CounterInfo> counter in loadtasks)
    //    {
    //        uiManager.CreateCounterFromLoad(counter.Result.name, counter.Result.counterValue, counter.Result.counterType, counter.Result.position);
    //    }
    //}
}
