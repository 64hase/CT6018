using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class TaskList : MonoBehaviour
{
    [SerializeField] private GameObject TaskListstackBase;
    [SerializeField] private Stack TaskListstackster;
    private int TaskNumber
    {
        get { return PlayerPrefs.GetInt("TaskNumber"); }
        set { PlayerPrefs.SetInt("TaskNumber", value); }
    }
    private GameObject TaskDateText;
    private GameObject TaskDescriptionText;
    private GameObject TaskPriority;
    public int TaskListNumber;
    public int archivesize;
    private Text TaskStarCount;
    [SerializeField] private Button TaskListOpenButton;
    [SerializeField] private Button TaskListCloseButton;
    [SerializeField] private Canvas TaskListCanvas;
    private OpenCloseWindows OpenCloseWindows;
    public int spawnedTaskListElements = 0;
    [SerializeField] private RectTransform content;

    //Creates new stack
    private void Awake()
    {
        TaskListstackster = new Stack();
    }

    //On start, spawn atleast 2 elements, then apply listeners for buttons.
    private void Start()
    {
        Debug.Log("Adding listener");
        TaskListOpenButton.onClick.AddListener(OnTaskListOpen);
        TaskListCloseButton.onClick.AddListener(OnTaskListClose);
        OpenCloseWindows = EventSystem.current.GetComponent<OpenCloseWindows>();
    }

    //Spawns a task in the tasklist if the size of the archive in playerprefs has not yet been reached.
    private void SpawnTaskListElement(int elementNumber)
    {
        if (spawnedTaskListElements == archivesize)
        {
            Debug.Log("Stack limit has been reached!");
            return;
        }
        else
        {
                spawnedTaskListElements++;
                GameObject instantiatedStackElement = Instantiate(TaskListstackBase, content);
                instantiatedStackElement.name = "TaskArchive_" + spawnedTaskListElements;
                ((RectTransform)instantiatedStackElement.transform).SetAsFirstSibling();
                TaskListstackster.Push(instantiatedStackElement);
                TaskListElement element = instantiatedStackElement.GetComponent<TaskListElement>();
                string playerPrefString = "ArchivedTask_" + elementNumber;
                Debug.Log("PlayerPrefString: " + PlayerPrefs.GetString(playerPrefString));
                string[] TaskInfoSplit = PlayerPrefs.GetString(playerPrefString).Split(',');
                DateTime date = DateTime.Parse(TaskInfoSplit.Length > 2 ? TaskInfoSplit[1] : DateTime.Now.ToString());
                element.Init(date.ToShortDateString(), TaskInfoSplit[0], TaskInfoSplit.Length > 2 ? TaskInfoSplit[2] : string.Empty, TaskInfoSplit.Length > 3 ? int.Parse(TaskInfoSplit[3]) : 0);
        }
    }

    //Assigned to a button, this updates the task list to match the
    private void UpdateTaskList()
    {
        GameObject[] TaskListObjs = GameObject.FindGameObjectsWithTag("TaskList");
        Debug.Log("AmountOfTasks: " + (archivesize + 1));
        for (int i = spawnedTaskListElements; i < archivesize; i++)
        {
                SpawnTaskListElement(i);
        }
    }
    private void OnTaskListOpen()
    {
        Debug.Log("OnEnable activated!");
        for (int i = spawnedTaskListElements; i < archivesize; i++)
        {
            SpawnTaskListElement(i);
        }
        OpenCloseWindows.OnWindowOpen(TaskListCanvas);
    }
    private void OnTaskListClose()
    {
        OpenCloseWindows.OnWindowClose(TaskListCanvas);
    }
}
