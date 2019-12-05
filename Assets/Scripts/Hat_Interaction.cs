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

    //On starts, this sets the scale of the tree, sets the animation bollean to false and also states that the object that this script is the component of is the hat!
    private void Start()
    {
        AnimationInProgress = false;
        SetTreeScale();
        PlayerTreeHat = this.gameObject;
    }
    //Saves the current scale of the hat
    public void SetTreeScale()
    {
        HatScale = this.transform.localScale;
        HatScaleUp = new Vector3(HatScale.x * 1.1F, HatScale.y * 1.1F, HatScale.z * 1.1F);
    }
    //On being pressed, the hat scales up and then scales back down
    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (AnimationInProgress == false)
            {
                AnimationInProgress = true;
                PlayerStage = EventSystem.current.GetComponent<ProgressManager>().PlayerStage;
                LeanTween.scale(PlayerTreeHat, new Vector3(HatScaleUp.x,HatScaleUp.y,HatScaleUp.z), 0.15F).setEaseInOutQuint();
                LeanTween.delayedCall(0.15f, EaseOutAnimation);
            }
        }
    }
    //Scales the hat down for the second half of the interaction animation
    private void EaseOutAnimation()
    {
        LeanTween.scale(PlayerTreeHat, new Vector3(HatScale.x, HatScale.y, HatScale.z), 0.15F).setEaseInOutQuint();
        AnimationInProgress = false;
    }
}
