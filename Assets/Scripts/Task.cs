using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Task : MonoBehaviour
{
    [SerializeField]
    private int TaskPointValue = 2;
    private string InputFieldText;
    public GameObject TaskElementBackground;
    public Text TaskTextRef;
    public Text TaskPlaceHolderTextRef;
    public InputField TaskInputField;
    private IEnumerator coroutine;
    private Button button;
    public Color colorToAnimateBackgroundTo;
    private int CoinCalculation;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
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
            PlayerPrefs.SetFloat("Progress", PlayerPrefs.GetFloat("Progress", 0) + TaskPointValue);
            CoinCalculation = PlayerPrefs.GetInt("PlayerCoinAmount") + 5;
            PlayerPrefs.SetInt("PlayerCoinAmount", CoinCalculation);
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
