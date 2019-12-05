using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourManager : MonoBehaviour
{
    public static ColourManager Instance;

    [SerializeField] public Color UI_Primary_Colour_LIGHT;
    [SerializeField] public Color UI_Secondary_Colour_LIGHT;
    [SerializeField] public Color UI_Text_LIGHT;
    [SerializeField] public ColorBlock UI_Button_LIGHT;
    [SerializeField] public Color colorStartLIGHT;
    [SerializeField] public Color colorEndLIGHT;
    [SerializeField] public Color UI_Primary_Colour_DARK;
    [SerializeField] public Color UI_Secondary_Colour_DARK;
    [SerializeField] public Color UI_Text_DARK;
    [SerializeField] public ColorBlock UI_Button_DARK;
    [SerializeField] public Color colorStartDARK;
    [SerializeField] public Color colorEndDARK;
    [SerializeField] public bool DarkModeEnabled;
    [SerializeField] public GameObject NightModeButton;
    [SerializeField] public ColourOperator ColourOperatorScript;
    private Button NightModeButtonComponent;

    //Event to trigger dark mode changing
    public delegate void OnDarkModeChanged();
    public static OnDarkModeChanged DarkModeChanged;

    private void Awake()
    {
        //Sets instance to this if no reference can be found on awake, otheriwse, destroy this.
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        //Sets listeners for buttons
        NightModeButtonComponent = NightModeButton.GetComponent<Button>();

        NightModeButtonComponent.onClick.AddListener(() => SetDarkMode(!DarkModeEnabled));
    }
    private void SetDarkMode(bool on)
    {
        //Enables darkmode
        DarkModeEnabled = on;
        DarkModeChanged?.Invoke();
    }
}
