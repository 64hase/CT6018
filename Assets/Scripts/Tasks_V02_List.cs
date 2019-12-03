using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class Tasks_V02_List : MonoBehaviour
{
    [SerializeField] private GameObject TaskstackBase;
    [SerializeField] private Stack Taskstackster;
    private int TaskNumber
    {
        get { return PlayerPrefs.GetInt("TaskNumber"); }
        set { PlayerPrefs.SetInt("TaskNumber", value); }
    }
    private GameObject TaskDateText;
    private GameObject TaskDescriptionText;
    private GameObject TaskPriority;
    public int TaskV02Number;
    private Text TaskStarCount;
    [SerializeField] private Button TaskUpdateButton;
    public int spawnedTaskElements = 0;
    [SerializeField] private RectTransform content;
    [SerializeField] private GameObject[] TaskListObjs;
    [SerializeField] private Button UpdateNameButton;
    [SerializeField] private Text TasksLeft;
    private GameObject TaskRef;
    [SerializeField] private List<string> TasksArchive;

    private void Awake()
    {
        Taskstackster = new Stack();
    }
    // Start is called before the first frame update
    private void Start()
    {
        OnUpdateByName();
        for (int i = spawnedTaskElements; i < TaskNumber; i++)
        {
            SpawnTask(i);
        }
        UpdateNameButton.onClick.AddListener(OnUpdateByName);
    }
    public void PopTaskButton()
    {
        GameObject temp;
        temp = (GameObject)Taskstackster.Pop();
        GameObject.Destroy(temp);
    }
    public void SpawnTask(int elementNumber)
    {
        if (spawnedTaskElements >= TaskNumber)
        {
            Debug.Log("Stack limit has been reached!");
            return;
        }
        else
        {
            spawnedTaskElements++;
            GameObject instantiatedStackElement = Instantiate(TaskstackBase, content);
            instantiatedStackElement.name = "Task_" + spawnedTaskElements;
            ((RectTransform)instantiatedStackElement.transform).SetAsFirstSibling();
            Taskstackster.Push(instantiatedStackElement);
            Task_V02 element = instantiatedStackElement.GetComponent<Task_V02>();
            string playerPrefString = "Task_" + elementNumber;
            Debug.Log("PlayerPrefString: " + PlayerPrefs.GetString(playerPrefString));
            string[] TaskInfoSplit = PlayerPrefs.GetString(playerPrefString).Split(',');
            element.OnTaskUpdate(TaskInfoSplit[0], TaskInfoSplit.Length > 1 ? TaskInfoSplit[1] : string.Empty, TaskInfoSplit.Length > 2 ? TaskInfoSplit[2] : string.Empty, TaskInfoSplit.Length > 3 ? int.Parse(TaskInfoSplit[3]) : 0, TaskInfoSplit.Length > 4 ? TaskInfoSplit[4] : string.Empty);
        } // TaskPlayerPrefExport = TaskDescriptionText.text + "," + DateTime.Now + "," + Priority + "," + StarCount.ToString() + "," + DueDate;
    }

    private void OnUpdateByName()
    {
        TasksLeft.text = TaskNumber + " left";
        EventSystem.current.GetComponent<TaskManager>().OnTaskAdded();
        TaskListObjs = GameObject.FindGameObjectsWithTag("Task");
        for (int i = 0; i < TaskListObjs.Length; i++)
        {
            TaskListObjs[i].GetComponent<Task_V02>().OnUpdateFromObjectName();
        }
    }
    //Get all new task data
    //Go through each stack element in a for loop
    //Check for the task id 
    //if task id exists: update data
    //if task id doesn't exist: delete stack element
    //then check to see if all ids in the stack match up to the new task data
    //if one exists in the stack but doesn't in the new task data then delete that task from the stack
    public void OnTaskComplete(GameObject Task)
    {
        Debug.Log("TaskV02List has received OnTaskComplete()");
        string TaskName = Task.gameObject.name.ToString();
        LeanTween.delayedCall(1F, DestroyTask);
        TasksArchive.Add(PlayerPrefs.GetString(Task.gameObject.name));
        OnTaskArchiveCreate();
        PlayerPrefs.DeleteKey(Task.gameObject.name);
        TaskRef = Task;
    }
    private void DestroyTask()
    {
        GameObject.Destroy(TaskRef);
    }
    private void OnTaskArchiveCreate()
    {
        for (int i = 0; i < TasksArchive.Count; i++)
        {
            PlayerPrefs.SetString("ArchivedTask_" + i, TasksArchive[i]);
        }
    }
}
