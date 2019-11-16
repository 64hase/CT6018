using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hat_Interaction : MonoBehaviour
{
    private GameObject PlayerTreeHat;
    private bool AnimationInProgress;
    private int PlayerStage;
    private Vector3 HatScale;
    private Vector3 HatScaleUp;
    // Start is called before the first frame update
    private void Start()
    {
        AnimationInProgress = false;
        SetTreeScale();
        PlayerTreeHat = this.gameObject;
    }
    public void SetTreeScale()
    {
        HatScale = this.transform.localScale;
        HatScaleUp = new Vector3(HatScale.x * 1.1F, HatScale.y * 1.1F, HatScale.z * 1.1F);
    }

    private void OnMouseOver()
    {
        Debug.Log("MouseHoverOnHat");
        if (Input.GetMouseButtonDown(0))
        {
            if (AnimationInProgress == false)
            {
                AnimationInProgress = true;
                Debug.Log("MousePressedOnHat");

                PlayerStage = EventSystem.current.GetComponent<ProgressManager>().PlayerStage;


                LeanTween.scale(PlayerTreeHat, new Vector3(HatScaleUp.x,HatScaleUp.y,HatScaleUp.z), 0.15F).setEaseInOutQuint();
                LeanTween.delayedCall(0.15f, EaseOutAnimation);
            }
        }
    }
    private void EaseOutAnimation()
    {
        LeanTween.scale(PlayerTreeHat, new Vector3(HatScale.x, HatScale.y, HatScale.z), 0.15F).setEaseInOutQuint();
        AnimationInProgress = false;
    }
}
