using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public const string KEY_PROGRESS = "Progress";
    public const string KEY_PROGRESSAIM = "ProgressAim";
    public float Progress
    {
        get { return PlayerPrefs.GetFloat(KEY_PROGRESS);  }
        set { PlayerPrefs.SetFloat(KEY_PROGRESS, value); }
    }
    public float ProgressAim
    {
        get { return PlayerPrefs.GetFloat(KEY_PROGRESSAIM); }
        set { PlayerPrefs.SetFloat(KEY_PROGRESSAIM, value); }
    }
    //For referencing the PrpgressFill Image used o the progress bar
    [SerializeField] private Image ProgressFill;
    //Use for referencing ProgressText
    [SerializeField] private Text ProgressText;
    //Used to convert Text element to String to allow for Length calculation
    [SerializeField] private string ProgressTextString;
    //Allows for default values for progress/progressAim to be toggled On/Off
    [SerializeField] private bool UseDefaults;
    [SerializeField] private GameObject PlayerTree;
    private GameObject[] Tree;
    [SerializeField] private GameObject Tree00;
    [SerializeField] private GameObject Tree01;
    [SerializeField] private GameObject Tree02;
    [SerializeField] private GameObject Tree03;

    //Stage number used to track the stage the player is currently on
    public int Stagenumber = 0;
    //Used for referencing the HAT Placeholder empty gameobject
    [SerializeField] private GameObject HatPlaceHolder;

    private void Start()
    {
        Tree[0] = Tree00;
        Tree[1] = Tree01;
        Tree[2] = Tree02;
        Tree[3] = Tree03;

        if (UseDefaults == true)
        {
            Progress = 0;
            ProgressAim = 10;
            Stagenumber = 0;
        }

    }
    public void UpdateProgress()
    {
        //Controls the fill of the progress bar based on the player's progress and progress aim
        ProgressFill.fillAmount = Progress / ProgressAim;

        //Formats the progress bar text to remain updated with the current progress and aim of the player
        ProgressTextString = string.Format(Progress + "/" + ProgressAim);
        ProgressText.GetComponent<Text>().text = ProgressTextString;

        //Once player has reached their progress aim, the aim is updated via multiplication.
        if (Progress == ProgressAim)
        {
            TreeGrowthEvent();
        }
    }

    private void TreeGrowthEvent()
    {
        //Disables current tree and enables next tree after stagenumber has been increased.
        Tree[Stagenumber].SetActive(false);
        Stagenumber = Stagenumber + 1;
        Tree[Stagenumber].SetActive(true);
    }
}
