using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [SerializeField]
    private GameObject StoreCanvas;
    private Button StoreExitButton;
    private Button StoreAccessButton;
    public GameObject hat;
    private CanvasGroup StoreCanvasGroup;
    [SerializeField] private GameObject StoreItemGameObject;
    [SerializeField] private Button StoreAlert;
    private bool AnimationHasPlayed = false;
    // Start is called before the first frame update
    private void Start()
    {
        StoreCanvas = GameObject.Find("Customisation_Shop");
        StoreExitButton = GameObject.Find("Store_Exit_Button").GetComponent<Button>();
        StoreAccessButton = GameObject.Find("StoreButton").GetComponent<Button>();
        StoreCanvas.SetActive(false);
        StoreExitButton.onClick.AddListener(OnClickClose);
        StoreAccessButton.onClick.AddListener(OnClickOpen);
        StoreCanvas.LeanAlpha(0, 0f);
        StoreCanvasGroup = StoreCanvas.GetComponent<CanvasGroup>();

    }
    private void OnClickOpen()
    {
        LeanTween.delayedCall(1f, DelayedScaleDown);
        StoreCanvas.SetActive(true);
        LeanTween.alphaCanvas(StoreCanvasGroup, 0, 0f);
        LeanTween.alphaCanvas(StoreCanvasGroup, 1, 1f).setEaseInOutQuint().setDelay(0.2f);

    }
    private void OnClickClose()
    {
        LeanTween.scale(hat, new Vector3(0.6F, 0.6F, 0.6F), 1.0F).setEaseOutElastic().setDelay(1f);
        LeanTween.alphaCanvas(StoreCanvasGroup, 0, 1f).setEaseInOutQuint();
        LeanTween.delayedCall(2f, DeactivateStore);
    }

    private void DelayedScaleDown()
    {
        LeanTween.scale(hat, new Vector3(0, 0, 0), 0F);
    }
    private void DeactivateStore()
    {
        StoreCanvas.SetActive(false);
    }

    private void Update()
    {
        if (StoreItemGameObject.GetComponent<Store_Item>().CanAfford == true)
        {
            if (AnimationHasPlayed == false)
            {
                LeanTween.delayedCall(10F, SetAnimCounter);
            }
        }
    }
    private void SetAnimCounter()
    {
        LeanTween.rotateZ(StoreAlert.gameObject, -20, 0.5F);
        LeanTween.rotateZ(StoreAlert.gameObject, 20, 0.5F).setDelay(0.5F);
        LeanTween.rotateZ(StoreAlert.gameObject, -20, 0.5F).setDelay(1F);
        LeanTween.rotateZ(StoreAlert.gameObject, 20, 0.5F).setDelay(1.5F);
        LeanTween.rotateZ(StoreAlert.gameObject, -20, 0.5F).setDelay(2.0F);
        LeanTween.rotateZ(StoreAlert.gameObject, 20, 0.5F).setDelay(2.5F);
        LeanTween.rotateZ(StoreAlert.gameObject, 0, 0.5F).setDelay(3.0F);
        LeanTween.delayedCall(3F, SetAnimCounter);
    }
}
