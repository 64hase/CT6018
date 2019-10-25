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
    [SerializeField]
    //For referencing the PrpgressFill Image used o the progress bar
    public Image ProgressFill;
    //The multiplier applied to ProgressAim once it has been reached by the player
    private float AimMultiplier = 2.5F;
    //Use for referencing ProgressText
    public Text ProgressText;
    //Used to convert Text element to String to allow for Length calculation
    private string ProgressTextString;
    //Allows for default values for progress/progressAim to be toggled On/Off
    public bool UseDefaults;
    public GameObject PlayerTree;
    //For referencing Tree Stage 01 Mesh
    public Mesh Tree_Stage01;
    //For referencing Tree Stage 02 Mesh
    public Mesh Tree_Stage02;
    //For referencing Tree Stage 03 Mesh
    public Mesh Tree_Stage03;
    //Stage number used to track the stage the player is currently on
    public int Stagenumber = 1;
    //Used for referencing the HAT Placeholder empty gameobject
    public GameObject HatPlaceHolder;
    // Start is called before the first frame update
    private void Start()
    {
        if (UseDefaults == true)
        {
            Progress = 0;
            ProgressAim = 10;
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
        if (Progress >= ProgressAim)
        {
            Stagenumber = Stagenumber + 1;
            //Checks which tree the player currently has and then updates to the next stage mesh accordingly.
            PlayerPrefs.SetFloat("ProgressAim", ProgressAim * AimMultiplier);

            //If player's progress equates to stage 02 of the game, then update tree MeshFilter to stage02 mesh
            if (Stagenumber == 2)
            {
                PlayerTree.GetComponent<MeshFilter>().mesh = Tree_Stage02;
                HatPlaceHolder.transform.position = HatPlaceHolder.transform.position + new Vector3(0, 1.2F, 0);
            }

            //If player's progress equates to stage 03 of the game, the update tree MeshFilter to stage03 mesh
            if (Stagenumber == 3)
            {
                PlayerTree.GetComponent<MeshFilter>().mesh = Tree_Stage03;
                HatPlaceHolder.transform.position = new Vector3(0,0, 0.2F);
            }
        }
    }
}
