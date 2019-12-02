using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenCloseWindows : MonoBehaviour
{
    public void OnWindowOpen(Canvas TargetCanvas)
    {
        if (TargetCanvas.isActiveAndEnabled)
        {
            Debug.Log("Opening window" + TargetCanvas.name);
            CanvasGroup CanvasAlpha = TargetCanvas.GetComponent<CanvasGroup>();
            CanvasAlpha.alpha = 0;
            TargetCanvas.gameObject.SetActive(true);
            LeanTween.alphaCanvas(CanvasAlpha, 1, 0.5F);
        }
    }
    public void OnWindowClose(Canvas TargetCanvas)
    {
        if (TargetCanvas.isActiveAndEnabled == false)
        {
            Debug.Log("Closing window" + TargetCanvas.name);
            CanvasGroup CanvasAlpha = TargetCanvas.GetComponent<CanvasGroup>();
            CanvasAlpha.alpha = 1;
            TargetCanvas.gameObject.SetActive(true);
            LeanTween.alphaCanvas(CanvasAlpha, 0, 0.5F);
        }
    }
}
