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
    //The multiplier applied to ProgressAim once it has been reached by the player
    [SerializeField] private float AimMultiplier = 2.5F;
    //Use for referencing ProgressText
    [SerializeField] private Text ProgressText;
    //Used to convert Text element to String to allow for Length calculation
    [SerializeField] private string ProgressTextString;
    //Allows for default values for progress/progressAim to be toggled On/Off
    [SerializeField] private bool UseDefaults;
    [SerializeField] private GameObject PlayerTree;
    [SerializeField] private GameObject Tree_Stage00;
    //For referencing Tree Stage 01 Gameobject
    [SerializeField] private GameObject Tree_Stage01;
    //For referencing Tree Stage 02 Gameobject
    [SerializeField] private GameObject Tree_Stage02;
    //For referencing Tree Stage 03 Gameobject
    [SerializeField] private GameObject Tree_Stage03;
    //Stage number used to track the stage the player is currently on
    public int Stagenumber = 0;
    //Used for referencing the HAT Placeholder empty gameobject
    [SerializeField] private GameObject HatPlaceHolder;
    // Start is called before the first frame update
    private bool Stage00Completed = false;
    private bool Stage01Completed = false;
    private bool Stage02Completed = false;




    private void Start()
    {
        if (UseDefaults == true)
        {
            Progress = 0;
            ProgressAim = 10;
            Stagenumber = 0;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        

        //Controls the fill of the progress bar based on the player's progress and progress aim
        ProgressFill.fillAmount = Progress / ProgressAim;

        //Formats the progress bar text to remain updated with the current progress and aim of the player
        ProgressTextString = string.Format(Progress+"/"+ProgressAim);
        ProgressText.GetComponent<Text>().text = ProgressTextString;

        //Once player has reached their progress aim, the aim is updated via multiplication.
        if (Progress == ProgressAim)
        {

            if (Stage00Completed == false)
            {
                Tree_Stage00.transform.position = new Vector3(-40, 0, 0);
                Tree_Stage00.SetActive(false);
                Tree_Stage01.transform.position = new Vector3(0, 8.844F, 0);
                Tree_Stage01.SetActive(true);
                PlayerPrefs.SetFloat("ProgressAim", ProgressAim * AimMultiplier);
                Stage00Completed = true;
            }

            if (Stage00Completed == true)
            {
                if (Stage01Completed == false)
                {
                    Tree_Stage01.transform.position = new Vector3(-40, 0, 0);
                    Tree_Stage01.SetActive(false);
                    Tree_Stage02.transform.position = new Vector3(0, 8.844F, 0);
                    Tree_Stage02.SetActive(true);
                    PlayerPrefs.SetFloat("ProgressAim", ProgressAim * AimMultiplier);
                    Stage01Completed = true;
                }
            }
        }
    }
}
