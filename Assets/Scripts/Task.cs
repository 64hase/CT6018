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
    [SerializeField] private Text TaskTextRef;
    [SerializeField] private Text TaskPlaceHolderTextRef;
    [SerializeField] private InputField TaskInputField;
    [SerializeField] private Color colorToAnimateBackgroundTo;

    private int TaskPointValue = 2;
    private string InputFieldText;
    private IEnumerator coroutine;
    private Button button;
    private float ProgressManager;
    private int Stagenumber;
    private GameObject EventSystemObject;

    private ProgressBar progressBar;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        EventSystemObject = EventSystem.current.gameObject;
    }

    private void OnClick()
    {
        //Change the task background tint to green to indicate it has been completed
        TaskElementBackground.GetComponent<Image>().color = colorToAnimateBackgroundTo;

        //Change text colour to white so that it is more visible against the green tint
        TaskTextRef.GetComponent<UnityEngine.UI.Text>().color = Color.white;

        //Set task text to "+{points} Points!" to indicate to the player that they have earned points for completing task
        TaskTextRef.GetComponent<UnityEngine.UI.Text>().text = "+" + TaskPointValue + " points!";
        TaskPlaceHolderTextRef.GetComponent<UnityEngine.UI.Text>().text = "+" + TaskPointValue + " points!";

        //Increases the player's progress value by the value of the task they have just completed, IF, a task of more than 0 character was inputted.
        InputFieldText = TaskInputField.text;

        if (InputFieldText.Length > 0)
        {
            EventSystemObject.GetComponentInParent<ProgressManager>().UpdateProgress();
        }

        //Testing OnClick 
        Debug.Log("Test");

        //Starts the Coroutine after 2 seconds
        coroutine = WaitAndDestroy(2.0f);
        StartCoroutine(coroutine);
        //StartCoroutine(WaitAndDestroy(2.0f));

    }

    private IEnumerator WaitAndDestroy(float waitTime)
    {
        //Wait for 2 Seconds before executing
        yield return new WaitForSeconds(waitTime);

        //Destroy the parent object of the task button, aka. the task element itself. 
        DestroyImmediate(transform.parent.gameObject, true);
    }

}
