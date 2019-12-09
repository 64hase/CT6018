using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OpenCloseWindows : MonoBehaviour
{
    private Canvas TargetCanvasRef;
    public int OpenCount = 0;
    private UnityAdCaller UnityAdCaller;

    private void Start()
    {
       UnityAdCaller = EventSystem.current.GetComponent<UnityAdCaller>();
    }
    //Opens the referenced canvas with fade
    public void OnWindowOpen(Canvas TargetCanvas)
    {
        UnityAdCaller.OnShowInterstital();
        OpenCount++;
        TargetCanvasRef = TargetCanvas;
        if (TargetCanvas.isActiveAndEnabled == false)
        {
            TargetCanvas.GetComponent<Canvas>().sortingOrder = 7;
            Debug.Log("Opening window" + TargetCanvas.name);
            CanvasGroup CanvasAlpha = TargetCanvas.GetComponent<CanvasGroup>();
            CanvasAlpha.alpha = 0;
            TargetCanvas.gameObject.SetActive(true);
            LeanTween.alphaCanvas(CanvasAlpha, 1, 0.5F);
        }
    }

    //Closes referenced canvas with fade
    public void OnWindowClose(Canvas TargetCanvas)
    {
        TargetCanvasRef = TargetCanvas;
        if (TargetCanvas.isActiveAndEnabled == true)
        {
            Debug.Log("Closing window" + TargetCanvas.name);
            CanvasGroup CanvasAlpha = TargetCanvas.GetComponent<CanvasGroup>();
            CanvasAlpha.alpha = 1;
            LeanTween.alphaCanvas(CanvasAlpha, 0, 0.5F);
            LeanTween.delayedCall(0.5F, OnObjectSetActiveFalse);
        }
    }
    //Sets object active status back to false once fade animation duration is up.
    private void OnObjectSetActiveFalse()
    {
        TargetCanvasRef.GetComponent<Canvas>().sortingOrder = 0;
        TargetCanvasRef.gameObject.SetActive(false);
    }
}
