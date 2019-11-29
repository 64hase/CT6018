using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private GameObject StoreCanvas;
    [SerializeField] private Button StoreExitButton;
    [SerializeField] private Button StoreAccessButton;
    [SerializeField] private CanvasGroup StoreCanvasGroup;
    [SerializeField] private GameObject StoreItemGameObject;
    [SerializeField] private Button StoreAlert;
    [SerializeField] private GameObject[] PlayerTreeHat;
    private Vector3 HatScale;

    private bool AnimationIsPlaying = false;
    private GameObject PlayerTree;
    private int PlayerStage;

    // Start is called before the first frame update
    private void Start()
    {
        StoreCanvas.SetActive(false);
        StoreExitButton.onClick.AddListener(OnClickClose);
        StoreAccessButton.onClick.AddListener(OnClickOpen);
        StoreCanvas.LeanAlpha(0, 0f);
        StoreCanvasGroup = StoreCanvas.GetComponent<CanvasGroup>();
        PlayerStage = EventSystem.current.GetComponent<ProgressManager>().PlayerStage;
        PlayerTree = EventSystem.current.GetComponent<ProgressManager>().PlayerTreeGameObject;
        HatScale = PlayerTreeHat[PlayerStage].transform.localScale;
    }
    private void OnClickOpen()
    {
        //Stop animation
        LeanTween.cancel(StoreItemGameObject);

        PlayerStage = EventSystem.current.GetComponent<ProgressManager>().PlayerStage;
        HatScale = PlayerTreeHat[PlayerStage].transform.localScale;
        LeanTween.delayedCall(1f, DelayedScaleDown);
        StoreCanvas.SetActive(true);
        LeanTween.alphaCanvas(StoreCanvasGroup, 0, 0f);
        LeanTween.alphaCanvas(StoreCanvasGroup, 1, 1f).setEaseInOutQuint().setDelay(0.2f);

    }
    private void OnClickClose()
    {
        LeanTween.scale(PlayerTreeHat[PlayerStage], new Vector3(HatScale.x,HatScale.y,HatScale.z), 1.0F).setEaseOutElastic().setDelay(1f);
        LeanTween.alphaCanvas(StoreCanvasGroup, 0, 1f).setEaseInOutQuint();
        LeanTween.delayedCall(2f, DeactivateStore);
    }

    private void DelayedScaleDown()
    {
        LeanTween.scale(PlayerTreeHat[PlayerStage], new Vector3(0, 0, 0), 0F);
    }
    private void DeactivateStore()
    {
        StoreCanvas.SetActive(false);
    }

    private void Update()
    {
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
    /*private void SetAnimCounter()
    {
        LeanTween.rotateZ(StoreAlert.gameObject, -20, 0.5F);
        LeanTween.rotateZ(StoreAlert.gameObject, 20, 0.5F).setDelay(0.5F);
        LeanTween.rotateZ(StoreAlert.gameObject, -20, 0.5F).setDelay(1F);
        LeanTween.rotateZ(StoreAlert.gameObject, 20, 0.5F).setDelay(1.5F);
        LeanTween.rotateZ(StoreAlert.gameObject, -20, 0.5F).setDelay(2.0F);
        LeanTween.rotateZ(StoreAlert.gameObject, 20, 0.5F).setDelay(2.5F);
        LeanTween.rotateZ(StoreAlert.gameObject, 0, 0.5F).setDelay(3.0F);
        LeanTween.delayedCall(3F, SetAnimCounter);
    }*/
}
