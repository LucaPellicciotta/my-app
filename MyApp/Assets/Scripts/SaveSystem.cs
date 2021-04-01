using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;

public static class SaveSystem
{
    private static FirebaseDatabase database;
    private static DatabaseReference databaseReference;

    public static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";
    public static readonly string SAVE_EXTENSION = ".txt";
    public static readonly string COUNTER_SAVE_PATH = "/Counters/";

    public static int childCount;

    public static void Init()
    {
        database = FirebaseDatabase.DefaultInstance;

        // Set up events
        FirebaseDatabase.DefaultInstance.GetReference(COUNTER_SAVE_PATH).ValueChanged += OnValueChanged;

        if (!Directory.Exists(SAVE_FOLDER))
            Directory.CreateDirectory(SAVE_FOLDER);
    }

    private static void OnValueChanged(object sender, ValueChangedEventArgs e)
    {
        UpdateChildCount(COUNTER_SAVE_PATH);
    }

    public static void DeleteAllData()
    {
        database.GetReference(COUNTER_SAVE_PATH).RemoveValueAsync();
    }

    public static void Save()
    {
        WriteDataLocally();
        WriteDataOnline();
    }

    public static void Load()
    {
        ReadDataLocally();
    }

    #region Write Data
    public static void WriteDataLocally()
    {
        string saveString = "";
        for (int i = 0; i < UIManager.counterInfos.Count; i++)
        {
            saveString += JsonUtility.ToJson(UIManager.counterInfos[i]);
            saveString += "\n";
        }
        Debug.Log(saveString);

        File.WriteAllText(SAVE_FOLDER + "save" + SAVE_EXTENSION, saveString);
    }

    public static void WriteDataOnline()
    {
        SaveSystem.DeleteAllData();

        int ii = 1;
        Debug.Log(COUNTER_SAVE_PATH + ii.ToString());

        Debug.Log(database.GetReference(COUNTER_SAVE_PATH + ii.ToString()));

        for (int i = 0; i < UIManager.counterInfos.Count; i++)
            database.GetReference(COUNTER_SAVE_PATH + i.ToString()).SetRawJsonValueAsync(JsonUtility.ToJson(UIManager.counterInfos[i]));
    }
    #endregion

    #region Read Data
    public static string ReadDataLocally()
    {
        if (File.Exists(SAVE_FOLDER + "save.txt"))
            return File.ReadAllText(SAVE_FOLDER + "save.txt");
        else
            throw new NullReferenceException();
    }

    public static async Task<CounterScript.CounterInfo> ReadDataOnline(string path)
    {
        var dataSnapshot = await database.GetReference(path).GetValueAsync();
        if (dataSnapshot.Value == null)
            return null;

        return JsonUtility.FromJson<CounterScript.CounterInfo>(dataSnapshot.GetRawJsonValue());
    }
    #endregion

    public static async void CreateLocalDataFromFirebase()
    {
        if (!File.Exists(SAVE_FOLDER + "save" + SAVE_EXTENSION))
        {
            var data = await ReadDataOnline(COUNTER_SAVE_PATH);
            Debug.Log(data);
        }
    }

    public static int GetChildCount()
    {
        return childCount;
    }

    public static async void UpdateChildCount(string path)
    {
        var dataSnapshot = await database.GetReference(path).GetValueAsync();
        childCount = (int) dataSnapshot.ChildrenCount;
    }
}
