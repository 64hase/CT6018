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
    private Text TaskStarCount;
    [SerializeField] private Button TaskListUpdateButton;
    [SerializeField] private Button TaskListOpenButton;
    [SerializeField] private Button TaskListCloseButton;
    [SerializeField] private Canvas TaskListCanvas;
    private OpenCloseWindows OpenCloseWindows;

    //Debug - remove this later
    public int spawnedTaskListElements = 0;

    /*[SerializeField]
    private StackElement elementToInstantiate;*/

        [SerializeField] private RectTransform content;

    //(RectTransform)gameObject.transform;

    //

    private void Awake()
    {
        TaskListstackster = new Stack();
        TaskListstackster.Push(TaskListstackBase);
    }
    // Start is called before the first frame update
    private void Start()
    {
        for (int i = spawnedTaskListElements; i < 2; i++)
        {
            SpawnTaskListElement(i);
        }
        Debug.Log("Adding listener");
        TaskListUpdateButton.onClick.AddListener(UpdateTaskList);
        TaskListOpenButton.onClick.AddListener(OnTaskListOpen);
        TaskListCloseButton.onClick.AddListener(OnTaskListClose);
        OpenCloseWindows = EventSystem.current.GetComponent<OpenCloseWindows>();
    }
    public void PushTaskListButton()
    {
        //SpawnTaskListElement();
        Debug.Log("I pushed an item on the stack");
    }

    public void PopTaskListButton()
    {
        GameObject temp;
        temp = (GameObject)TaskListstackster.Pop();
        GameObject.Destroy(temp);
    }

    private void SpawnTaskListElement(int elementNumber)
    {
        if (spawnedTaskListElements > 15)
        {
            Debug.Log("Stack limit has been reached!");
            return;
        }
        else
        {
            spawnedTaskListElements++;
            GameObject instantiatedStackElement = Instantiate(TaskListstackBase, content);
            instantiatedStackElement.name = "TaskList_" + spawnedTaskListElements;
            ((RectTransform)instantiatedStackElement.transform).SetAsFirstSibling();
            TaskListstackster.Push(instantiatedStackElement);
            TaskListElement element = instantiatedStackElement.GetComponent<TaskListElement>();
            string playerPrefString = "Task_" + elementNumber;
            Debug.Log("PlayerPrefString: " + PlayerPrefs.GetString(playerPrefString));
            string[] TaskInfoSplit = PlayerPrefs.GetString(playerPrefString).Split(',');
            DateTime date = DateTime.Parse(TaskInfoSplit.Length > 2 ? TaskInfoSplit[1] : DateTime.Now.ToString());
            element.Init(date.ToShortDateString(), TaskInfoSplit[0],  TaskInfoSplit.Length > 2 ? TaskInfoSplit[2] : string.Empty, TaskInfoSplit.Length > 3 ? int.Parse(TaskInfoSplit[3]) : 0);
        }
    }
    private void UpdateTaskList()
    {
        GameObject[] TaskListObjs = GameObject.FindGameObjectsWithTag("TaskList");

        int amountOfTasks = PlayerPrefs.GetInt("TaskNumber");
        Debug.Log("AmountOfTasks: " + (amountOfTasks + 1));
        for (int i = 0; i < amountOfTasks; i++)
        {
            SpawnTaskListElement(i);
        }
    }
    private void OnTaskListOpen()
    {
        OpenCloseWindows.OnWindowOpen(TaskListCanvas);
    }
    private void OnTaskListClose()
    {
        OpenCloseWindows.OnWindowClose(TaskListCanvas);
    }
}
