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
    [SerializeField] private Camera PlayerCamera;
    [SerializeField] private Text StageNumber;
    [SerializeField] private Image LockIcon;
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
        get { return PlayerPrefs.GetFloat(KEY_PROGRESSAIM, 10); }
        set { PlayerPrefs.SetFloat(KEY_PROGRESSAIM, value); }
    }
    public int PlayerStage
    {
        get { return PlayerPrefs.GetInt(KEY_PLAYERSTAGE); }
        set { PlayerPrefs.SetInt(KEY_PLAYERSTAGE, value); }
    }
    private int TaskNumber
    {
        get { return PlayerPrefs.GetInt("TaskNumber"); }
        set { PlayerPrefs.SetInt("TaskNumber", value); }
    }
    private int LowPriorityTaskCount
    {
        get { return PlayerPrefs.GetInt("LowPriorityTaskCount", 0); }
        set { PlayerPrefs.SetInt("LowPriorityTaskCount", value); }
    }
    private int NormalPriorityTaskCount
    {
        get { return PlayerPrefs.GetInt("NormalPriorityTaskCount", 0); }
        set { PlayerPrefs.SetInt("NormalPriorityTaskCount", value); }
    }
    private int HighPriorityTaskCount
    {
        get { return PlayerPrefs.GetInt("HighPriorityTaskCount", 0); }
        set { PlayerPrefs.SetInt("HighPriorityTaskCount", value); }
    }

    // Resets to default values if enabled for testing, then updates the progress bar.
    private void Start()
    {
        if (PlayerStage == 0)
        {
            ProgressAim = 10;
        }
        if(UseDefaults == true)
        {
            for (int i = 0; i < TreeArray.Length; i++)
            {
                TreeArray[i].SetActive(false);
            }
            TreeArray[0].SetActive(true);
            Progress = 0;
            ProgressAim = 10;
            PlayerStage = 0;
            TaskNumber = 0;
            LowPriorityTaskCount = 0;
            NormalPriorityTaskCount = 0;
            HighPriorityTaskCount = 0;
            PlayerPrefs.DeleteAll();
        }
        OnAdjustCameraSize();
        ProgressBarFill.fillAmount = Progress / ProgressAim;
        ProgressBarText.text = string.Format(Progress + "/" + ProgressAim);
        for (int i = 0; i < TreeArray.Length; i++)
        {
            TreeArray[i].SetActive(false);
        }
        TreeArray[PlayerStage].SetActive(true);
        if(PlayerStage >= 3)
        {
            StageNumber.gameObject.SetActive(false);
            LockIcon.gameObject.SetActive(true);
        }
        else
        {
            StageNumber.gameObject.SetActive(true);
            LockIcon.gameObject.SetActive(false);
            StageNumber.text = (PlayerStage + 1).ToString();
        }
    }

    // Update the progress of the player
    public void UpdateProgress()
    {
        float initialProgress = Progress;
        Progress = Progress + TaskValue;
        TaskNumber = TaskNumber - 1;
        PlayerPrefs.SetInt("PlayerCoinAmount", PlayerPrefs.GetInt("PlayerCoinAmount", 0) + 5);

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
            ProgressAim = ProgressAim * 2;
            ProgressBarFill.fillAmount = Progress / ProgressAim;
            ProgressBarText.text = string.Format(Progress + "/" + ProgressAim);
        }
    }

    private void TreeGrowthEvent()
    {
        //Updates the tree to the next stage tree.
        LeanTween.scale(TreeArray[PlayerStage], new Vector3(0, 0, 0), 1F).setEaseInBounce();
        LeanTween.delayedCall(1F, OnDelayedSetActiveFalse);
        PlayerStage = Mathf.Clamp(PlayerStage + 1, 0, 3);
        StageNumber.text = (PlayerStage + 1).ToString();
        LeanTween.scale(TreeArray[PlayerStage], new Vector3(0, 0, 0), 0F);
        TreeArray[PlayerStage].SetActive(true);
        LeanTween.scale(TreeArray[PlayerStage], new Vector3(5, 5, 5), 1F).setEaseInBounce();
        TreeArray[PlayerStage].GetComponent<Animator>();
        Debug.Log("TreeGrowthEvent occured");
        PlayerTreeGameObject = TreeArray[PlayerStage].gameObject;
        PlayerTreeGameObject.GetComponentInChildren<Hat_Interaction>().SetTreeScale();
        OnAdjustCameraSize();
    }
    private void OnDelayedSetActiveFalse()
    {
        TreeArray[PlayerStage-1].SetActive(false);
    }
    private void OnAdjustCameraSize()
    {
        if (PlayerStage > 1)
        {
            float ZoomedOutSize = 10.5F;
            LeanTween.value(gameObject, PlayerCamera.orthographicSize, ZoomedOutSize, 0.5f).setOnUpdate((float value) =>
            {
                PlayerCamera.orthographicSize = value;
            }).setEase(LeanTweenType.easeOutSine);
        }
        else
        {
            float ZoomedOutSize = 8F;
            LeanTween.value(gameObject, PlayerCamera.orthographicSize, ZoomedOutSize, 0.5f).setOnUpdate((float value) =>
            {
                PlayerCamera.orthographicSize = value;
            }).setEase(LeanTweenType.easeOutSine);
        }
    }

}
