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
    public Image ProgressFill;
    private float AimMultiplier = 1.5F;
    public Text ProgressText;
    private string ProgressTextString;
    public bool UseDefaults;
    // Start is called before the first frame update
    private void Start()
    {
        if (UseDefaults == true)
        {
            Progress = 0;
            ProgressAim = 20;
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
            PlayerPrefs.SetFloat("ProgressAim", ProgressAim * AimMultiplier);
        }
    }
}
