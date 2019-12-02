using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourManager : MonoBehaviour
{
    //singleton
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
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        NightModeButtonComponent = NightModeButton.GetComponent<Button>();

        NightModeButtonComponent.onClick.AddListener(() => SetDarkMode(!DarkModeEnabled));
        NightModeButtonComponent.onClick.AddListener(OnSkyboxChange);
    }
    private void OnSkyboxChange()
    {
        float lerp = Mathf.PingPong(Time.time, 1.0F) / 1.0F;
        if (DarkModeEnabled)
        {
            Debug.Log("Working skybox alteration!");
            RenderSettings.skybox.SetColor("_SkyTint", colorStartDARK);
            RenderSettings.skybox.SetColor("_Ground", colorEndDARK);

        }
        else
        {
            RenderSettings.skybox.SetColor("_SkyTint", colorStartLIGHT);
            RenderSettings.skybox.SetColor("_Ground", colorEndLIGHT);
        }
    }

    private void SetDarkMode(bool on)
    {
        DarkModeEnabled = on;
        DarkModeChanged?.Invoke();
    }
}
