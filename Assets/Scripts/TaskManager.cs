using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private bool CleanOnStart;
    public List<string> TaskArray = new List<string>();
    // Start is called before the first frame update
    public void OnTaskAdded()
    {
        int TaskNumber = PlayerPrefs.GetInt("TaskNumber", 0);
        Debug.Log("OnTaskAdded invoked!");
        for (int i = 0; i < TaskNumber; i++)
        {
            TaskArray.Add(PlayerPrefs.GetString("Task_" + i));
        }
        if(CleanOnStart)
        {
            for (int i = 0; i < TaskNumber; i++)
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
