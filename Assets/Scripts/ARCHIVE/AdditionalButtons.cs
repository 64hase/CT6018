using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdditionalButtons : MonoBehaviour
{
    [SerializeField] private Button DarkMode;
    [SerializeField] private Button CreateTask;
    [SerializeField] private Button TaskList;
    [SerializeField] private Canvas TaskListCanvas;
    [SerializeField] private Canvas CreateTaskCanvas;

    // Start is called before the first frame update
    private void Start()
    {
        DarkMode.onClick.AddListener(OnDarkMode);
        CreateTask.onClick.AddListener(OnCreateTask);
        TaskList.onClick.AddListener(OnTaskList);
    }

    private void OnDarkMode()
    {

    }
    private void OnCreateTask()
    {
        CreateTaskCanvas.gameObject.SetActive(true);
        Debug.Log("CreateTaskCanvas enabled");
    }
    private void OnTaskList()
    {
        TaskListCanvas.gameObject.SetActive(true);
        Debug.Log("TaskListCanvas enabled");
    }
}
