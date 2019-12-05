using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class Create_Task : MonoBehaviour
{
    [SerializeField] private Text TaskDescriptionText;
    [SerializeField] private Text Day;
    [SerializeField] private Text Month;
    [SerializeField] private Text Year;
    [SerializeField] private Text Hour;
    [SerializeField] private Text Minute;
    [SerializeField] private Button LowPriority;
    [SerializeField] private Button MediumPriority;
    [SerializeField] private Button HighPriority;
    [SerializeField] private string TaskPlayerPrefExport;
    [SerializeField] private Button CreateButton;
    [SerializeField] private Button CancelButton;
    [SerializeField] private Canvas TaskCreateCanvas;
    [SerializeField] private string TaskDueDate;
    [SerializeField] private string[] SetDueDateToCurrent;
    [SerializeField] private Text OneStarHours;
    [SerializeField] private Text TwoStarsHours;
    [SerializeField] private Text ThreeStarsHours;
    [SerializeField] private Button[] ValueChangingButtons;
    [SerializeField] private Button OpenCreateTask;
    [SerializeField] private GameObject Tasks_V02_List;
    private GameObject[] TaskItems;
    public string[] TaskInfoSplit;
    private TimeSpan TaskTimeSpan;
    private int StarCount = 0;
    private string Priority;
    private string DueDate;
    private int TaskNumber
    {
        get { return PlayerPrefs.GetInt("TaskNumber"); }
        set { PlayerPrefs.SetInt("TaskNumber", value); }
    }

    // Adds listeners to buttons
    // Set create button to inactive at start
    // Runs OnTaskDueDate and OnSetDueDateToCurrent
    private void Start()
    {
        CreateButton.onClick.AddListener(OnTaskSaveData);
        CancelButton.onClick.AddListener(OnTaskCreateClose);
        LowPriority.onClick.AddListener(() => OnTaskPrioritySet(1));
        MediumPriority.onClick.AddListener(() => OnTaskPrioritySet(2));
        HighPriority.onClick.AddListener(() => OnTaskPrioritySet(3));
        for (int i = 0; i < ValueChangingButtons.Length; i++)
        {
            ValueChangingButtons[i].onClick.AddListener(OnValueChanged);
            ValueChangingButtons[i].onClick.AddListener(OnCheckData);
        }
        TaskItems = GameObject.FindGameObjectsWithTag("Task");
        CreateButton.interactable = false;
        OnTaskDueDateSet();
    }
    //On the create tasks window being enabled, the duedate is set to current and the current inputted data is checked.
    private void OnEnable()
    {
        OnSetDueDateToCurrent();
        OnCheckData();
    }
    // Sets the due date field to the current date and time
    private void OnSetDueDateToCurrent()
    {
        string CurrentDateTime;
        CurrentDateTime = DateTime.Now.ToString();
        SetDueDateToCurrent = CurrentDateTime.Split(',', '/', ':', ' ');
        Day.text = SetDueDateToCurrent[0];
        Month.text = SetDueDateToCurrent[1];
        int CurrentYear;
        CurrentYear = int.Parse(SetDueDateToCurrent[2]) - 2000;
        Year.text = CurrentYear.ToString();
        Hour.text = SetDueDateToCurrent[3];
        Minute.text = SetDueDateToCurrent[4];
    }
    //Sets the due date for saving to playerprefs
    private void OnTaskDueDateSet()
    {
        DueDate = Day.text + "/" + Month.text + "/" + Year.text + " " + Hour.text + ":" + Minute.text;
        TaskDueDate = DateTime.Parse(DueDate).ToString();
    }
    //Sets the priority based on the user's input
    private void OnTaskPrioritySet(int value)
    {
        if (value == 1)
        {
            Priority = "Low";
        }
        if (value == 2)
        {
            Priority = "Normal";
        }
        if (value == 3)
        {
            Priority = "High";
        }
    }
    //On the user changing a value in the due date, this sets the star hour requirements.
    private void OnValueChanged()
    {
        OnTaskDueDateSet();
        TaskTimeSpan = DateTime.Parse(DueDate) - DateTime.Now;
        float TaskHours = (float)TaskTimeSpan.TotalSeconds / 3600;
        float TaskHoursThird = Mathf.Clamp(Mathf.Ceil(TaskHours / 3), 0, 999);
        float TaskHoursTenth = Mathf.Clamp(Mathf.Ceil(TaskHours / 10), 0, 999);
        OneStarHours.text = (TaskHoursThird * 2) + (TaskHoursTenth * 2) + "+  hrs";
        TwoStarsHours.text = ((TaskHoursThird * 2) - TaskHoursTenth) + " - " + ((TaskHoursThird * 2) + TaskHoursTenth) + " hrs";
        ThreeStarsHours.text = TaskHoursThird.ToString();

    }
    //On user input being detected, the current data is checked to see if its viable as a task. Then enables create button if viable.
    private void OnCheckData()
    {
        OnTaskDueDateSet();
        TaskPlayerPrefExport = TaskDescriptionText.text + "," + DateTime.Now.ToShortDateString() + "," + Priority + "," + StarCount.ToString() + "," + DueDate;
        if (string.IsNullOrEmpty(TaskPlayerPrefExport[0].ToString()) == false)
        {
            if (string.IsNullOrEmpty(TaskPlayerPrefExport[1].ToString()) == false)
            {
                if (string.IsNullOrEmpty(TaskPlayerPrefExport[2].ToString()) == false)
                {
                    if (string.IsNullOrEmpty(TaskPlayerPrefExport[3].ToString()) == false)
                    {
                        if (string.IsNullOrEmpty(TaskPlayerPrefExport[4].ToString()) == false)
                        {
                            CreateButton.interactable = true;
                        }
                    }
                }
            }
        }
    }

    //Upon pressing create, this saves the inputted data in a string format to playerprefs. Then spawns a new task item in task list with the new task information
    private void OnTaskSaveData()
    {
        OnTaskDueDateSet();
        TaskNumber = Mathf.Clamp((TaskNumber + 1), 1, 1000);
        if (PlayerPrefs.HasKey("Task_" + TaskNumber) == false)
        {
            TaskPlayerPrefExport = TaskDescriptionText.text + "," + DateTime.Now.ToShortDateString() + "," + Priority + "," + StarCount.ToString() + "," + DueDate;
            PlayerPrefs.SetString("Task_" + TaskNumber, TaskPlayerPrefExport);
            TaskInfoSplit = TaskPlayerPrefExport.Split(',');
            Tasks_V02_List.GetComponent<Tasks_V02_List>().SpawnTask(TaskNumber);
            OnTaskCreateClose();
            //TaskItems[TaskNumber].GetComponent<Task_V02>().OnTaskUpdate(TaskInfoSplit[0], TaskInfoSplit[1], TaskInfoSplit[2], TaskInfoSplit[3], TaskInfoSplit[4]);
        }
        else
        {
            Debug.Log("Task ID already exists!");
        }
    }

    //For closing the create tasks window.
    private void OnTaskCreateClose()
    {
        EventSystem.current.GetComponent<OpenCloseWindows>().OnWindowClose(TaskCreateCanvas);
    }
}
