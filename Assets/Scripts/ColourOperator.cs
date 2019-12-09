using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ColourOperator : MonoBehaviour
{
    private GameObject EventSystemGameObject;
    private bool DarkModeEnabled;
    private InputField thisInputField;
    private Text InputFieldText;
    private GameObject NightModeButton;
    private Button NightModeButtonComponent;

    private Text textComp;
    private Image imageComp;
    private InputField ifComp;
    private Text ifTextComp;
    private Color OriginalColour;

    // Start is called before the first frame update
    void Start()
    {
        //Sets listeners and checks which object type the gameobject that this script is attached to is.
        //Updates colours
        EventSystemGameObject = EventSystem.current.gameObject;
        DarkModeEnabled = ColourManager.Instance.DarkModeEnabled;
        NightModeButton = ColourManager.Instance.NightModeButton;
        NightModeButtonComponent = NightModeButton.GetComponent<Button>();
        NightModeButtonComponent.onClick.AddListener(OnColourUpdate);
        textComp = GetComponent<Text>();
        imageComp = GetComponent<Image>();
        ifComp = GetComponent<InputField>();
        if(ifComp != null)
        {
            ifTextComp = ifComp.GetComponent<Text>();
        }
        if (textComp != null)
        {
            OriginalColour = this.GetComponent<Text>().color;
        }
        if (imageComp != null)
        {
            OriginalColour = this.GetComponent<Image>().color;
        }
        if (ifComp != null)
        {
        }
        ColourManager.DarkModeChanged += OnColourUpdate;
        OnColourUpdate();
    }

    private void OnDestroy()
    {
        //On being destroyed, reset colour of object
        ColourManager.DarkModeChanged -= OnColourUpdate;
    }

    //Updates all the colours of on dark mode being enabled or disabled.
    public void OnColourUpdate()
    {
        DarkModeEnabled = ColourManager.Instance.DarkModeEnabled;

        Text textComp = GetComponent<Text>();
        if (textComp != null)
        {
            LeanTween.value(gameObject, textComp.color, DarkModeEnabled ? ColourManager.Instance.UI_Text_DARK : OriginalColour, 1f).setEase(LeanTweenType.easeInOutSine).setOnUpdate((Color value) =>
            {
                textComp.color = value;
            });
        }
        if (imageComp != null)
        {
            LeanTween.value(gameObject, imageComp.color, DarkModeEnabled ? ColourManager.Instance.UI_Primary_Colour_DARK : OriginalColour, 1f).setEase(LeanTweenType.easeInOutSine).setOnUpdate((Color value) =>
            {
                imageComp.color = value;
            });
        }
        if (ifComp != null)
        {
            thisInputField = ifComp;
            InputFieldText = ifTextComp;
            if (InputFieldText != null)
            {
                InputFieldText.color = DarkModeEnabled ? ColourManager.Instance.UI_Text_DARK : OriginalColour;
            }
        }
    }
}
