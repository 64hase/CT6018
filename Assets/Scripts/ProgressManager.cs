using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProgressManager : MonoBehaviour
{
    [SerializeField]private GameObject[] TreeArray;

    public const string KEY_PROGRESS = "Progress";
    public const string KEY_PROGRESSAIM = "ProgressAim";
    public const string KEY_PLAYERSTAGE = "PlayerStage";
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

    [SerializeField] private Image ProgressBarFill;
    [SerializeField] private Text ProgressBarText;
    [SerializeField] private bool UseDefaults;
    private int TaskValue = 2;
    private bool FirstTime;
    public GameObject PlayerTreeGameObject;


    // Start is called before the first frame update
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
        Progress = Progress + TaskValue;
        ProgressBarFill.fillAmount = Progress / ProgressAim;
        ProgressBarText.text = string.Format(Progress + "/" + ProgressAim);

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
        TreeArray[PlayerStage].SetActive(false);
        PlayerStage = PlayerStage + 1;
        TreeArray[PlayerStage].SetActive(true);
        TreeArray[PlayerStage].GetComponent<Animator>();
        Debug.Log("TreeGrowthEvent occured");
        PlayerTreeGameObject = TreeArray[PlayerStage].gameObject;
        PlayerTreeGameObject.GetComponentInChildren<Hat_Interaction>().SetTreeScale();
    }
}
