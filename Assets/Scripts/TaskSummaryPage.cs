using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TaskSummaryPage : MonoBehaviour
{
    private int TaskNumber
    {
        get { return PlayerPrefs.GetInt("TaskNumber"); }
        set { PlayerPrefs.SetInt("TaskNumber", value); }
    }
    private string TaskList;
    private string[] TaskInfoSplit;
    [SerializeField] private Text TaskListText;
    [SerializeField] private GameObject UpdateButton;

    private void Start()
    {

        UpdateButton.GetComponent<Button>().onClick.AddListener(OnUpdated);
    }
    void OnUpdated()
    {
        TaskList = "Task   Creation Date   Priority/n";
        for (int i = 0; i < TaskNumber; i++)
        {
            TaskInfoSplit = PlayerPrefs.GetString("Task_" + i).Split(',');
            TaskList = TaskList + TaskInfoSplit[0] + " " + TaskInfoSplit[1] + " " + TaskInfoSplit[2] +"\n";
            TaskListText.text = TaskList;
        }
    }

}
