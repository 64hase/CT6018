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
    [SerializeField] private GameObject TaskV02ListCanvas;
    [SerializeField] private GameObject TaskListGameObject;
    private int LowPriorityTaskCount
    {
        get { return PlayerPrefs.GetInt("LowPriorityTaskCount", 0); }
        set { PlayerPrefs.SetInt("LowPriorityTaskCount", value); }
    }
    private int NormalPriorityTaskCount
    {
        get { return PlayerPrefs.GetInt("NormalPriorityTaskCount", 0); }
        set { PlayerPrefs.SetInt("NormalPriorityTaskCount", value); }
    }
    private int HighPriorityTaskCount
    {
        get { return PlayerPrefs.GetInt("HighPriorityTaskCount", 0); }
        set { PlayerPrefs.SetInt("HighPriorityTaskCount", value); }
    }
    private ProgressManager ProgressManager;
    private Tasks_V02_List TasksV02List;
    private TaskList TaskList;

    private void Start()
    {
        ProgressManager = EventSystem.current.GetComponent<ProgressManager>();
        OnUpdateFromObjectName();
        TaskCompleteButton.onClick.AddListener(OnTaskComplete);
        TasksV02List = TaskV02ListCanvas.GetComponent<Tasks_V02_List>();
        TaskList = TaskListGameObject.GetComponent<TaskList>();
    }
    public void OnUpdateFromObjectName()
    {
        string[] TaskInfoSplit = PlayerPrefs.GetString(this.gameObject.name.ToString()).Split(',');
        TaskDescription.text = TaskInfoSplit.Length > 1 ? TaskInfoSplit[0] : "Null";
        Priority.text = TaskInfoSplit.Length > 2 ? TaskInfoSplit[2] : "Null";
        StarCount.text = TaskInfoSplit.Length > 3 ? TaskInfoSplit[3] : "Null";
        DueDate.text = TaskInfoSplit.Length > 4 ? TaskInfoSplit[4] : "Null";
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
            HighPriorityTaskCount = HighPriorityTaskCount + 1;
        }
        if (Priority.text == "Normal")
        {
            PriorityPanel.color = PriorityMediumColour;
            NormalPriorityTaskCount = NormalPriorityTaskCount + 1;
        }
        if (Priority.text == "Low")
        {
            PriorityPanel.color = PriorityLowColour;
            LowPriorityTaskCount = LowPriorityTaskCount + 1;
        }
    }
    private void OnTaskComplete()
    {
        //When task is completed, update the progress and archive. Then initiate task complete in Task list v02 script.
        ProgressManager.UpdateProgress();
        TaskList.archivesize++;
        TasksV02List.OnTaskComplete(this.gameObject);
        if (Priority.text == "High")
        {
            HighPriorityTaskCount = HighPriorityTaskCount - 1;
        }
        if (Priority.text == "Normal")
        {
            Debug.Log("Normal text set");
            NormalPriorityTaskCount = NormalPriorityTaskCount - 1;
        }
        if (Priority.text == "Low")
        {
            LowPriorityTaskCount = LowPriorityTaskCount - 1;
        }
    }
}
