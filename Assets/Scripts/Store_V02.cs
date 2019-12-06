using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Store_V02 : MonoBehaviour
{
    [SerializeField] private Sprite[] Sprites;
    [SerializeField] private int[] Prices;
    [SerializeField] private Mesh[] Hats;
    [SerializeField] private Button StoreExitButton;
    [SerializeField] private Button StoreAccessButton;
    [SerializeField] private Canvas StoreCanvas;
    private CanvasGroup StoreCanvasGroup;
    [SerializeField] private Image StoreAlert;
    public GameObject[] PlayerTreeHat;
    [SerializeField] private GameObject[] StoreItems;
    [SerializeField] private GameObject TaskListsV02;
    private Tasks_V02_List Task_V02_List;
    private Vector3 HatScale;
    private bool AnimationIsPlaying = false;
    private GameObject PlayerTree;
    private int PlayerStage;
    private OpenCloseWindows OpenCloseWindows;
    private bool CanAfford;
    public bool ResetHat;
    private Mesh HatToSetTo;
    private int PlayerCoins
    {
        get { return PlayerPrefs.GetInt("PlayerCoinAmount"); }
        set { PlayerPrefs.SetInt("PlayerCoinAmount", value); }
    }

    // Start is called before the first frame update
    private void Start()
    {
        OpenCloseWindows = EventSystem.current.GetComponent<OpenCloseWindows>();
        StoreCanvasGroup = StoreCanvas.GetComponent<CanvasGroup>();
        OnCheckIfCanAfford();
        Task_V02_List = TaskListsV02.GetComponent<Tasks_V02_List>();
        StoreAccessButton.onClick.AddListener(OnClickOpen);
        StoreExitButton.onClick.AddListener(OnClickClose);
        for (int i = 0; i < StoreItems.Length; i++)
        {
            //Creates a OwnsHat bool, then checks to see if the hat is owned by the player, then set data for the storeitem.
            bool OwnsHat;
            if (PlayerPrefs.GetInt("OwnsHat_" + Hats[i].name) == 1) { OwnsHat = true; } else { OwnsHat = false; }
            StoreItems[i].GetComponent<Store_V02_Item>().OnSetStoreData(Sprites[i], Hats[i], Prices[i], OwnsHat);
        }
    }


    //Sets hat scale for when store is opened.
    private void OnClickOpen()
    {
        PlayerStage = EventSystem.current.GetComponent<ProgressManager>().PlayerStage;
        HatScale = PlayerTreeHat[PlayerStage].transform.localScale;
        LeanTween.delayedCall(1f, DelayedScaleDown);
        Task_V02_List.IsStore = true;
        HatToSetTo = PlayerTreeHat[PlayerStage].GetComponent<MeshFilter>().mesh;
        OnMinimize();
    }
    private void DelayedScaleDown()
    {
        //LeanTween.scale(PlayerTreeHat[PlayerStage], new Vector3(0, 0, 0), 0.5F);
        OpenCloseWindows.OnWindowOpen(StoreCanvas);
    }


    //Scales hat back up once store is closed.
    private void OnClickClose()
    {
        LeanTween.scale(PlayerTreeHat[PlayerStage], new Vector3(HatScale.x, HatScale.y, HatScale.z), 1.0F).setEaseOutElastic().setDelay(1f);
        OpenCloseWindows.OnWindowClose(StoreCanvas);
        Task_V02_List.IsStore = false;
        LeanTween.delayedCall(0.5F, OnMinimize);
        if (ResetHat == true) { OnResetHat(); }
    }
    private void OnResetHat()
    {
        PlayerTreeHat[PlayerStage].GetComponent<MeshFilter>().mesh = HatToSetTo;
        ResetHat = false;
    }
    private void OnMinimize()
    {
        Task_V02_List.OnMinimize();
    }
    private void OnCheckIfCanAfford()
    {
        for (int i = 0; i < StoreItems.Length; i++)
        {
            if (Prices[i] < PlayerCoins)
            {
                CanAfford = true;
            }
            else
            {
                CanAfford = false;
            }
        }
    }

    //Animates store alert if the player can afford an item in the store
    private void Update()
    {
        if (CanAfford == true)
        {
            if (AnimationIsPlaying == false)
            {
                AnimationIsPlaying = true;
                LeanTween.value(StoreAlert.gameObject, 0, 40, 0.5f).setEase(LeanTweenType.easeInOutSine).setOnUpdate((float value) =>
                {
                    float rotationAmount = value - 20;
                    Vector3 rotation = StoreAlert.transform.eulerAngles;
                    rotation.z = rotationAmount;
                    StoreAlert.transform.eulerAngles = rotation;

                }).setLoopPingPong();
            }
        }
    }
}
