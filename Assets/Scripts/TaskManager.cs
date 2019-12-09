using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TaskManager : MonoBehaviour
{
    //Used for viewing current task information in editor
    public static TaskManager Instance;
    private string[] DateToCheck;
    private TimeSpan Difference;

    [SerializeField] private bool CleanOnStart;
    public List<string> TaskArray = new List<string>();

    private int TaskNumber
    {
        get { return PlayerPrefs.GetInt("TaskNumber"); }
        set { PlayerPrefs.SetInt("TaskNumber", value); }
    }

    private void Awake()
    {
        //Ensures this is the only instance.
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
        OnTaskAdded();
    }

    public void OnTaskAdded()
    {
        //Creates an array of all the current tasks
        TaskArray.Clear();
        for (int i = 0; i < TaskNumber; i++)
        {
            TaskArray.Add(PlayerPrefs.GetString("Task_" + i));
        }
        if(CleanOnStart)
        {
            //Clears all task playerpref data on start if enabled.
            for (int i = 0; i < 30; i++)
            {
                PlayerPrefs.DeleteKey("Task_" + i);
                PlayerPrefs.SetInt("TaskNumber", 0);
            }
        }
    }
}
