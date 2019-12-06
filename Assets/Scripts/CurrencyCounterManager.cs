using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyCounterManager : MonoBehaviour
{
    private int CoinAmount;
    private int GemAmount;
    [SerializeField] private Text CoinCounterText;
    [SerializeField] private Image CoinPanel;
    [SerializeField] private Text GemCounterText;
    [SerializeField] private Image GemPanel;
    [SerializeField] private Color MaintenanceColour;
    [SerializeField] private Color DefaultColor;
    [SerializeField] private bool ResetOnStartup;
    [SerializeField] private bool CoinMaintenanceEnabled;
    [SerializeField] private bool GemMaintenanceEnabled;
    // On start, set all the values of the counters based on conditions
    private void Start()
    {
        PlayerPrefs.GetInt("PlayerCoinAmount", 0);
        PlayerPrefs.GetInt("PlayerGemAmount", 0);

        if (ResetOnStartup == true)
        {
            PlayerPrefs.SetInt("PlayerCoinAmount", 500);
        }
        if (CoinMaintenanceEnabled == false)
        {
            CoinCounterText.text = "" + PlayerPrefs.GetInt("PlayerCoinAmount");
            CoinPanel.GetComponent<Image>().color = DefaultColor;
        }
        if (CoinMaintenanceEnabled == true)
        {
            CoinCounterText.text = "";
            CoinPanel.GetComponent<Image>().color = MaintenanceColour;
        }

        if (GemMaintenanceEnabled == false)
        {
            GemCounterText.text = "" + PlayerPrefs.GetInt("PlayerGemAmount");
            CoinPanel.GetComponent<Image>().color = DefaultColor;
        }
        if (GemMaintenanceEnabled == true)
        {
            GemCounterText.text = "";
            GemPanel.GetComponent<Image>().color = MaintenanceColour;
        }
    }

    // Continuously sets the value so that if teh values changes it remains updated.
    private void Update()
    {
        if (CoinMaintenanceEnabled == false)
        {
            CoinCounterText.GetComponent<UnityEngine.UI.Text>().text = PlayerPrefs.GetInt("PlayerCoinAmount").ToString();
        }
        if (GemMaintenanceEnabled == false)
        {
            GemCounterText.GetComponent<UnityEngine.UI.Text>().text = PlayerPrefs.GetInt("PlayerGemAmount").ToString();
        }
    }

}
