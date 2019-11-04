using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat_Interaction : MonoBehaviour
{
    public GameObject Hat;
    private bool AnimationInProgress;
    // Start is called before the first frame update
    private void Start()
    {
        AnimationInProgress = false;
    }

    private void OnMouseOver()
    {
        Debug.Log("MouseHoverOnHat");
        if (Input.GetMouseButtonDown(0))
        {
            if (AnimationInProgress == false)
            {
                Debug.Log("MousePressedOnHat");
                LeanTween.scale(Hat, new Vector3(0.55F, 0.55F, 0.55F), 0.15f).setEaseInOutQuint();
                LeanTween.delayedCall(0.15f, EaseOutAnimation);
            }
        }
    }
    private void EaseOutAnimation()
    {
        LeanTween.scale(Hat, new Vector3(0.5F, 0.5F, 0.5F), 0.15F).setEaseInOutQuint();
        AnimationInProgress = false;
    }
}
