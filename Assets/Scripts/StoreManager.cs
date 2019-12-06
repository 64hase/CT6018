using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private GameObject StoreGameObject;
    private Canvas StoreCanvas;
    [SerializeField] private Button StoreExitButton;
    [SerializeField] private Button StoreAccessButton;
    [SerializeField] private CanvasGroup StoreCanvasGroup;
    [SerializeField] private GameObject StoreItemGameObject;
    [SerializeField] private Image StoreAlert;
    [SerializeField] private GameObject[] PlayerTreeHat;
    private Vector3 HatScale;
    private bool AnimationIsPlaying = false;
    private GameObject PlayerTree;
    private int PlayerStage;
    private OpenCloseWindows OpenCloseWindows;

    // Start is called before the first frame update
    private void Start()
    {
        //Sets variables on start
        OpenCloseWindows = EventSystem.current.GetComponent<OpenCloseWindows>();
        StoreCanvas = StoreGameObject.GetComponent<Canvas>();
        StoreGameObject.SetActive(false);
        StoreExitButton.onClick.AddListener(OnClickClose);
        StoreAccessButton.onClick.AddListener(OnClickOpen);
        StoreCanvasGroup = StoreCanvas.GetComponent<CanvasGroup>();
        PlayerStage = EventSystem.current.GetComponent<ProgressManager>().PlayerStage;
        PlayerTree = EventSystem.current.GetComponent<ProgressManager>().PlayerTreeGameObject;
        HatScale = PlayerTreeHat[PlayerStage].transform.localScale;
    }
    private void OnClickOpen()
    {
        //Sets hat scale for when store is opened.
        LeanTween.cancel(StoreItemGameObject);
        PlayerStage = EventSystem.current.GetComponent<ProgressManager>().PlayerStage;
        HatScale = PlayerTreeHat[PlayerStage].transform.localScale;
        OpenCloseWindows.OnWindowOpen(StoreCanvas);
        LeanTween.delayedCall(1f, DelayedScaleDown);
    }
    private void DelayedScaleDown()
    {
        LeanTween.scale(PlayerTreeHat[PlayerStage], new Vector3(0, 0, 0), 0F);
    }
    private void OnClickClose()
    {
        //Scales hat back up once store is closed.
        LeanTween.scale(PlayerTreeHat[PlayerStage], new Vector3(HatScale.x,HatScale.y,HatScale.z), 1.0F).setEaseOutElastic().setDelay(1f);
        OpenCloseWindows.OnWindowClose(StoreCanvas);
    }

    private void Update()
    {
        //Animates store alert if the player can afford an item in the store
        if (StoreItemGameObject.GetComponent<Store_Item>().CanAfford == true)
        {
            if (AnimationIsPlaying == false)
            {
                AnimationIsPlaying = true;

                //Animation
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
