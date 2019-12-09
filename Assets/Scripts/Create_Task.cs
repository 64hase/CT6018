using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using System.Threading;

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
    [SerializeField] private DateTime TaskDueDate;
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
    private TaskManager TaskManager;
    private int TaskNumber
    {
        get { return PlayerPrefs.GetInt("TaskNumber"); }
        set { PlayerPrefs.SetInt("TaskNumber", value); }
    }
    private DateTime OneStarDate;
    private DateTime TwoStarDate;
    private DateTime ThreeStarDate;

    private void Awake()
    {
        Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-GB");
    }

    // Adds listeners to buttons
    // Set create button to inactive at start
    // Runs OnTaskDueDate and OnSetDueDateToCurrent
    private void Start()
    {
        //DateTime.

        CreateButton.onClick.AddListener(OnTaskSaveData);
        CancelButton.onClick.AddListener(OnTaskCreateClose);
        LowPriority.onClick.AddListener(() => OnTaskPrioritySet(1));
        MediumPriority.onClick.AddListener(() => OnTaskPrioritySet(2));
        HighPriority.onClick.AddListener(() => OnTaskPrioritySet(3));
        TaskItems = GameObject.FindGameObjectsWithTag("Task");
        CreateButton.interactable = false;
        OnSetDueDateToCurrent();
        OnTaskDueDateSet();
        TaskManager = EventSystem.current.GetComponent<TaskManager>();
    }
    //On the create tasks window being enabled, the duedate is set to current and the current inputted data is checked.
    private void OnEnable()
    {
        TaskDescriptionText.text = string.Empty;
        OnSetDueDateToCurrent();
        OnCheckData();
        OnValueChanged();
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
        TaskDueDate = DateTime.Parse(DueDate);
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
    public void OnValueChanged()
    {
        OnTaskDueDateSet();
        TaskTimeSpan = TaskDueDate.Subtract(DateTime.Now);
        float ThreeStarHourCount = Mathf.Ceil(Mathf.Clamp((float.Parse((TaskTimeSpan.TotalHours * 0.25).ToString())), 0, 999));
        float TwoStarHourCount = Mathf.Ceil(Mathf.Clamp((float.Parse((TaskTimeSpan.TotalHours * 0.5).ToString())), 0, 999));
        float OneStarHourCount = Mathf.Ceil(Mathf.Clamp((float.Parse((TaskTimeSpan.TotalHours * 0.75).ToString())), 0, 999));
        OneStarHours.text = "< " + OneStarHourCount + "  hrs";
        TwoStarsHours.text = "< " + TwoStarHourCount + " hrs";
        ThreeStarsHours.text = "< " + ThreeStarHourCount + " hrs";
        OneStarDate = DateTime.Now.AddHours(OneStarHourCount);
        TwoStarDate = DateTime.Now.AddHours(TwoStarHourCount);
        ThreeStarDate = DateTime.Now.AddHours(ThreeStarHourCount);
    }
    //On user input being detected, the current data is checked to see if its viable as a task. Then enables create button if viable.
    public void OnCheckData()
    {
        OnTaskDueDateSet();
        TaskPlayerPrefExport = TaskDescriptionText.text + "," + DateTime.Now.ToShortDateString() + "," + Priority + "," + StarCount.ToString() + "," + DueDate + "," + OneStarDate + "," + TwoStarDate + "," + ThreeStarDate;
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
            TaskPlayerPrefExport = TaskDescriptionText.text + "," + DateTime.Now.ToShortDateString() + "," + Priority + "," + StarCount.ToString() + "," + DueDate + "," + OneStarDate.ToString() + "," + TwoStarDate.ToString() + "," + ThreeStarDate.ToString();
            PlayerPrefs.SetString("Task_" + TaskNumber, TaskPlayerPrefExport);
            TaskInfoSplit = TaskPlayerPrefExport.Split(',');
            Tasks_V02_List.GetComponent<Tasks_V02_List>().SpawnTask(TaskNumber);
            OnTaskCreateClose();
            TaskManager.OnTaskAdded();
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
        if(TaskNumber > 6)
        {
            Tasks_V02_List.GetComponent<Tasks_V02_List>().OnBottomShadow();
        }
    }
}
