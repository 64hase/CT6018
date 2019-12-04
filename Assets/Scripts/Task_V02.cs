using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class Task_V02 : MonoBehaviour
{
    [SerializeField] private string[] TaskPlayerPrefExport;
    [SerializeField] private Text TaskDescription;
    //[SerializeField] private Text DateTimeCreation;
    [SerializeField] private Text DueDate;
    [SerializeField] private Text Priority;
    [SerializeField] private Image PriorityPanel; 
    [SerializeField] private Text StarCount;
    [SerializeField] private Color PriorityHighColour;
    [SerializeField] private Color PriorityMediumColour;
    [SerializeField] private Color PriorityLowColour;
    [SerializeField] private Button TaskCompleteButton;
    private ProgressManager ProgressManager;
    private Tasks_V02_List TasksV02List;
    private TaskList TaskList;

    private void Start()
    {
        ProgressManager = EventSystem.current.GetComponent<ProgressManager>();
        OnUpdateFromObjectName();
        TaskCompleteButton.onClick.AddListener(OnTaskComplete);
        TasksV02List = EventSystem.current.GetComponent<Tasks_V02_List>();
        TaskList = EventSystem.current.GetComponent<TaskList>();
    }
    public void OnUpdateFromObjectName()
    {
        string[] TaskInfoSplit = PlayerPrefs.GetString(this.gameObject.name).Split(',');
        TaskDescription.text = TaskInfoSplit[0];
        Priority.text = TaskInfoSplit[2];
        StarCount.text = TaskInfoSplit[3];
        DueDate.text = TaskInfoSplit[4];
        Debug.Log("oioi - " + this.gameObject.name + ": " + TaskDescription.text + "," + Priority.text + "," + StarCount.text + "," + DueDate.text);
    }
    public void OnTaskUpdate(string TaskDescriptionUpdate, string DateTimeUpdate, string PriorityUpdate, int StarCountUpdate, string DueDateUpdate)
    {
        // TaskPlayerPrefExport = TaskDescriptionText.text + "," + DateTime.Now + "," + Priority + "," + StarCount.ToString() + "," + DueDate;
        TaskDescription.text = TaskDescriptionUpdate;
        //DateTimeCreation.text = DateTimeUpdate;
        Priority.text = PriorityUpdate;
        StarCount.text = StarCountUpdate.ToString();
        DueDate.text = DueDateUpdate;

        if (Priority.text == "High")
        {
            PriorityPanel.color = PriorityHighColour;
        }
        if (Priority.text == "Medium")
        {
            PriorityPanel.color = PriorityMediumColour;
        }
        if (Priority.text == "Low")
        {
            PriorityPanel.color = PriorityLowColour;
        }
    }
    private void OnTaskComplete()
    {
        Debug.Log("Task has received OnTaskCOmplete()");
        ProgressManager.UpdateProgress();
        TaskList.archivesize++;
        TasksV02List.OnTaskComplete(this.gameObject);
    }
}
