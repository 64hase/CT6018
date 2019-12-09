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
    [SerializeField] private Button TaskUpdateButton;
    [SerializeField] private RectTransform content;
    [SerializeField] private GameObject[] TaskListObjs;
    [SerializeField] private Button UpdateNameButton;
    [SerializeField] private Text TasksLeft;
    [SerializeField] private List<string> TasksArchive;
    [SerializeField] private GameObject MinimizedObjects;
    [SerializeField] private Button MinimizeButton;
    [SerializeField] private Image MinimizeButtonImg;
    [SerializeField] private Sprite PointUpSprite;
    [SerializeField] private Sprite PointDownSprite;
    [SerializeField] private Button CreateTaskButton;
    [SerializeField] private GameObject PlayerCameraController;
    [SerializeField] private Image BottomShadow;
    [SerializeField] private GameObject TaskArchive;
    private int TaskNumber
    {
        get { return PlayerPrefs.GetInt("TaskNumber"); }
        set { PlayerPrefs.SetInt("TaskNumber", value); }
    }
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
    private GameObject TaskDateText;
    private GameObject TaskDescriptionText;
    private GameObject TaskPriority;
    public int TaskV02Number;
    private Text TaskStarCount;
    private bool Minimized = false;
    private GameObject TaskRef;
    public int spawnedTaskElements = 0;
    public bool IsStore;
    private int TaskArchiveSize;
    private string[] DateToCheck;
    private TimeSpan Difference;
    private string TaskConstruction;

    //On Awake, creates the stack
    private void Awake()
    {
        BottomShadow.enabled = false;
        Taskstackster = new Stack();
        OnUpdateByName();
    }
    // On start, spawns enough tasks that equate to the number of tasks the player has saved in playerprefs.
    // Sets the taks data of each task based on info from playerprefs
    //Applies listeners for buttons
    private void Start()
    {
        TaskArchiveSize = LowPriorityTaskCount + NormalPriorityTaskCount + HighPriorityTaskCount;
        OnBottomShadow();
        for (int i = spawnedTaskElements; i < TaskNumber; i++)
        {
            SpawnTask(i);
        }
        OnUpdateByName();
        UpdateNameButton.onClick.AddListener(OnUpdateByName);
        MinimizeButton.onClick.AddListener(OnMinimize);
    }
    //Used to remove an item from the stack
    public void PopTaskButton()
    {
        GameObject temp;
        temp = (GameObject)Taskstackster.Pop();
        GameObject.Destroy(temp);
    }
    //Spawns a task into the stack
    public void SpawnTask(int elementNumber)
    {
        if (spawnedTaskElements > TaskNumber)
        {
            //If the limit has been reached for the stack, then an error message is produced.
            Debug.Log("Stack limit has been reached!");
            return;
        }
        else
        {
            //Otherwise, spawn a task with information from playerprefs.
            spawnedTaskElements++;
            GameObject instantiatedStackElement = Instantiate(TaskstackBase, content);
            instantiatedStackElement.name = "Task_" + spawnedTaskElements;
            ((RectTransform)instantiatedStackElement.transform).SetAsFirstSibling();
            Taskstackster.Push(instantiatedStackElement);
            Task_V02 element = instantiatedStackElement.GetComponent<Task_V02>();
            string playerPrefString = "Task_" + elementNumber;
            string[] TaskInfoSplit = PlayerPrefs.GetString(playerPrefString).Split(',');
            element.OnTaskUpdate(TaskInfoSplit[0], TaskInfoSplit.Length > 1 ? TaskInfoSplit[1] : string.Empty, TaskInfoSplit.Length > 2 ? TaskInfoSplit[2] : string.Empty, TaskInfoSplit.Length > 3 ? int.Parse(TaskInfoSplit[3]) : 0, TaskInfoSplit.Length > 4 ? TaskInfoSplit[4] : string.Empty);
        } // TaskPlayerPrefExport = TaskDescriptionText.text + "," + DateTime.Now + "," + Priority + "," + StarCount.ToString() + "," + DueDate;
    }

    private void OnUpdateByName()
    {
        TasksLeft.text = TaskNumber + " left";
        if (TaskNumber > 0)
        {
            //Updates the task items based on playerprefs that contain the same name
            OnBottomShadow();
            EventSystem.current.GetComponent<TaskManager>().OnTaskAdded();
            TaskListObjs = GameObject.FindGameObjectsWithTag("Task");
            for (int i = 1; i < TaskNumber; i++)
            {
                string[] PlayerprefSplit;
                //TaskListObjs[i].GetComponent<Task_V02>().OnUpdateFromObjectName();
                PlayerprefSplit = PlayerPrefs.GetString("Task_" + i).Split(',');
                TaskListObjs[i].GetComponent<Task_V02>().OnTaskUpdate(PlayerprefSplit[0], PlayerprefSplit.Length > 1 ? PlayerprefSplit[1] : string.Empty, PlayerprefSplit.Length > 2 ? PlayerprefSplit[2] : string.Empty, PlayerprefSplit.Length > 3 ? int.Parse(PlayerprefSplit[3]) : 0, PlayerprefSplit.Length > 4 ? PlayerprefSplit[4] : string.Empty);
            }
        }
        else
        {
            Debug.Log("There are no tasks");
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
        string TaskName = Task.gameObject.name.ToString();
        LeanTween.delayedCall(1F, DestroyTask);

            DateToCheck = PlayerPrefs.GetString(TaskName).Split(',');
            Difference = DateTime.Now.Subtract(DateTime.Parse(DateToCheck[7]));
            if (Difference.Seconds > 1)
            {
                Difference = DateTime.Now.Subtract(DateTime.Parse(DateToCheck[6]));
                if (Difference.Seconds > 1)
                {
                    //Three stars earned
                    TaskConstruction = DateToCheck[0] + "," + DateToCheck[1] + "," + DateToCheck[2] + "," + "3" + "," + DateToCheck[4] + "," + DateToCheck[5] + "," + DateToCheck[6] + "," + DateToCheck[7];
                    PlayerPrefs.SetString(TaskName, TaskConstruction);
                Debug.Log("Three stars have been earned.  " + TaskConstruction);
                }
                else
                {
                    //Two stars earned
                    TaskConstruction = DateToCheck[0] + "," + DateToCheck[1] + "," + DateToCheck[2] + "," + "2" + "," + DateToCheck[4] + "," + DateToCheck[5] + "," + DateToCheck[6] + "," + DateToCheck[7];
                    PlayerPrefs.SetString(TaskName, TaskConstruction);
                Debug.Log("Two stars have been earned.  " + TaskConstruction);
                }
            }
            else
            {
                //One stars earned
                TaskConstruction = DateToCheck[0] + "," + DateToCheck[1] + "," + DateToCheck[2] + "," + "1" + "," + DateToCheck[4] + "," + DateToCheck[5] + "," + DateToCheck[6] + "," + DateToCheck[7];
                PlayerPrefs.SetString(TaskName, TaskConstruction);
            Debug.Log("One star has been earned!  " + TaskConstruction);
            }




        TasksArchive.Add(PlayerPrefs.GetString(Task.gameObject.name));
        OnTaskArchiveCreate();
        PlayerPrefs.DeleteKey(Task.gameObject.name);
        TaskRef = Task;
        OnBottomShadow();
        TaskArchiveSize++;
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
    //Minimizes the tasks panel 
    public void OnMinimize()
    {
        //Creates the variables required for minimizing the task panel
        float Anchorpos;
        float Scale;
        Vector3 CameraControllerPos;
        float CameraSize;
        MinimizeButtonImg.sprite = PointUpSprite;

        //Sets the values required for minimizing the task panel based on if the panel is currently minimized or not
        if (Minimized == true)
        {
            Anchorpos = -700F;
            Minimized = false;
            Scale = 1;
            CameraControllerPos = new Vector3(0, 9.01F, 0);
            CameraSize = 8.1F;
            CameraSize = 8.1F;
            MinimizeButtonImg.sprite = PointDownSprite;
        }
        else
        {
            if (IsStore == true) { Anchorpos = -2500F; } else { Anchorpos = -1981F; }
            Minimized = true;
            Scale = 0;
            CameraControllerPos = new Vector3(0, 11.5F, 0);
            CameraSize = 6.1F;
        }

        //Moves the task panel down into a minimized state
        RectTransform panelRect = (RectTransform)MinimizedObjects.transform;
        LeanTween.value(gameObject, panelRect.anchoredPosition.y, Anchorpos, 0.5f).setOnUpdate((float value) =>
        {
            panelRect.anchoredPosition = new Vector2(panelRect.anchoredPosition.x, value);
        }).setEase(LeanTweenType.easeOutSine);

        //Scales down the create task button if minimizing, then scales it up if expanding task panel
        LeanTween.scale(CreateTaskButton.gameObject, new Vector3(Scale, Scale, Scale), 0.5f).setEase(LeanTweenType.easeOutSine);

        //Sets Camera othorgraphic size 
        Camera camera = PlayerCameraController.GetComponentInChildren<Camera>();

        LeanTween.value(gameObject, camera.orthographicSize, CameraSize, 0.5f).setOnUpdate((float value) =>
        {
            camera.orthographicSize = value;
        }).setEase(LeanTweenType.easeOutSine);

        //Moves camera up for centralising the pillar and tree
        LeanTween.move(PlayerCameraController.gameObject, CameraControllerPos, 0.5f).setEase(LeanTweenType.easeOutSine);
    }
    public void OnBottomShadow()
    {
        //Enables a bottom shadow if there are enough tasks that some are off screen.
        if (TaskNumber > 6)
        {
            BottomShadow.enabled = true;
        }
    }
}
