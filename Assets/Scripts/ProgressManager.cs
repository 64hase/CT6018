using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProgressManager : MonoBehaviour
{
    [SerializeField]private GameObject[] TreeArray;
    [SerializeField] private Image ProgressBarFill;
    [SerializeField] private Text ProgressBarText;
    [SerializeField] private bool UseDefaults;
    public const string KEY_PROGRESS = "Progress";
    public const string KEY_PROGRESSAIM = "ProgressAim";
    public const string KEY_PLAYERSTAGE = "PlayerStage";
    private int TaskValue = 2;
    private bool FirstTime;
    public GameObject PlayerTreeGameObject;
    public float Progress
    {
        get { return PlayerPrefs.GetFloat(KEY_PROGRESS); }
        set { PlayerPrefs.SetFloat(KEY_PROGRESS, value); }
    }
    public float ProgressAim
    {
        get { return PlayerPrefs.GetFloat(KEY_PROGRESSAIM); }
        set { PlayerPrefs.SetFloat(KEY_PROGRESSAIM, value); }
    }
    public int PlayerStage
    {
        get { return PlayerPrefs.GetInt(KEY_PLAYERSTAGE); }
        set { PlayerPrefs.SetInt(KEY_PLAYERSTAGE, value); }
    }

    // Resets to default values if enabled for testing, then updates the progress bar.
    private void Start()
    {
        if(UseDefaults == true)
        {
            Progress = 8;
            ProgressAim = 10;
            PlayerStage = 0;
        }
        ProgressBarFill.fillAmount = Progress / ProgressAim;
        ProgressBarText.text = string.Format(Progress + "/" + ProgressAim);
        TreeArray[PlayerStage].SetActive(true);
    }

    // Update the progress of the player
    public void UpdateProgress()
    {
        float initialProgress = Progress;
        Progress = Progress + TaskValue;



        //Animates the updating of the progress bar to the new progress value.
        LeanTween.value(ProgressBarFill.gameObject, ProgressBarFill.fillAmount, Progress / ProgressAim, 1F).setOnUpdate((float value) =>
        {
            ProgressBarFill.fillAmount = value;
        });


        //Updates progress bar text
        ProgressBarText.text = string.Format(Progress + "/" + ProgressAim);

        //Initiates a TreeGrowthEvent if the progress of the player matches their aim.
        if (Progress >= ProgressAim)
        {
            TreeGrowthEvent();
            ProgressAim = ProgressAim + ProgressAim / 2;
            ProgressBarFill.fillAmount = Progress / ProgressAim;
            ProgressBarText.text = string.Format(Progress + "/" + ProgressAim);
        }
    }

    void TreeGrowthEvent()
    {
        //Updates the tree to the next stage tree.
        TreeArray[PlayerStage].SetActive(false);
        PlayerStage = PlayerStage + 1;
        TreeArray[PlayerStage].SetActive(true);
        TreeArray[PlayerStage].GetComponent<Animator>();
        Debug.Log("TreeGrowthEvent occured");
        PlayerTreeGameObject = TreeArray[PlayerStage].gameObject;
        PlayerTreeGameObject.GetComponentInChildren<Hat_Interaction>().SetTreeScale();
    }
}
