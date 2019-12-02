using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private bool CleanOnStart;
    public List<string> TaskArray = new List<string>();
    private int TaskNumber
    {
        get { return PlayerPrefs.GetInt("TaskNumber"); }
        set { PlayerPrefs.SetInt("TaskNumber", value); }
    }

    public void OnTaskAdded()
    {
        TaskArray.Clear();
        for (int i = 0; i < TaskNumber; i++)
        {
            TaskArray.Add(PlayerPrefs.GetString("Task_" + i));
        }
        if(CleanOnStart)
        {
            for (int i = 0; i < 30; i++)
            {
                PlayerPrefs.DeleteKey("Task_" + i);
                PlayerPrefs.SetInt("TaskNumber", 0);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
