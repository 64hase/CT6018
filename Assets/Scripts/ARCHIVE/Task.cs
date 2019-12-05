using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public class Task : MonoBehaviour
{
    [SerializeField] private GameObject TaskElementBackground;
    [SerializeField] private Color colorToAnimateBackgroundTo;

    private int TaskPointValue = 2;
    private Text InputFieldText;
    private IEnumerator coroutine;
    private Button button;
    private float ProgressManager;
    private int Stagenumber;
    private GameObject EventSystemObject;
    [SerializeField] private InputField TaskInputFieldObject;
    private InputField TaskInputField;
    private ProgressBar progressBar;
    private String TaskText;
    private int TaskNumber
    {
        get { return PlayerPrefs.GetInt("TaskNumber"); }
        set { PlayerPrefs.SetInt("TaskNumber", value); }
    }
    private string Priority = "Normal";
    private int StarCount = 0;
    private string TaskPlayerPref;
    private GameObject TaskManager;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        EventSystemObject = EventSystem.current.gameObject;
        TaskInputField = TaskInputFieldObject.GetComponent<InputField>();
        EventSystemObject.GetComponent<TaskManager>().OnTaskAdded();
    }

    private void OnClick()
    {
        //Change the task background tint to green to indicate it has been completed
        TaskElementBackground.GetComponent<Image>().color = colorToAnimateBackgroundTo;

        TaskText = TaskInputField.text;
        PlayerPrefs.SetString("Task_" + TaskNumber, TaskText + "," + DateTime.Now + "," + Priority + "," + StarCount);
        TaskNumber++;
        EventSystemObject.GetComponent<TaskManager>().OnTaskAdded();
        //Set task text to "+{points} Points!" to indicate to the player that they have earned points for completing task
        TaskInputField.text = "+" + TaskPointValue + " points!";

        //Increases the player's progress value by the value of the task they have just completed, IF, a task of more than 0 character was inputted.

        if (TaskInputField.text.Length > 0)
        {
            EventSystemObject.GetComponentInParent<ProgressManager>().UpdateProgress();
        }
        //Starts the Coroutine after 2 seconds
        coroutine = WaitAndDestroy(2.0f);
        StartCoroutine(coroutine);
        //StartCoroutine(WaitAndDestroy(2.0f));

    }
    private IEnumerator WaitAndDestroy(float waitTime)
    {
        //Wait for 2 Seconds before executing
        yield return new WaitForSeconds(waitTime);

        EventSystemObject.GetComponent<StackManager>().ShowShadow();

        //Destroy the parent object of the task button, aka. the task element itself. 
        DestroyImmediate(transform.parent.gameObject, true);
    }

}
