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
    // Start is called before the first frame update
    private void Start()
    {
        PlayerPrefs.GetInt("PlayerCoinAmount", 0);
        PlayerPrefs.GetInt("PlayerGemAmount", 0);

        if (ResetOnStartup == true)
        {
            PlayerPrefs.SetInt("PlayerCoinAmount", 0);
        }
        if (CoinMaintenanceEnabled == false)
        {
            CoinCounterText.GetComponent<UnityEngine.UI.Text>().text = "" + PlayerPrefs.GetInt("PlayerCoinAmount");
            CoinPanel.GetComponent<Image>().color = DefaultColor;
        }
        if (CoinMaintenanceEnabled == true)
        {
            CoinCounterText.GetComponent<UnityEngine.UI.Text>().text = "";
            CoinPanel.GetComponent<Image>().color = MaintenanceColour;
        }

        if (GemMaintenanceEnabled == false)
        {
            GemCounterText.GetComponent<UnityEngine.UI.Text>().text = "" + PlayerPrefs.GetInt("PlayerGemAmount");
            CoinPanel.GetComponent<Image>().color = DefaultColor;
        }
        if (GemMaintenanceEnabled == true)
        {
            GemCounterText.GetComponent<UnityEngine.UI.Text>().text = "";
            GemPanel.GetComponent<Image>().color = MaintenanceColour;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (CoinMaintenanceEnabled == false)
        {
            CoinCounterText.GetComponent<UnityEngine.UI.Text>().text = "" + PlayerPrefs.GetInt("PlayerCoinAmount");
        }
        if (GemMaintenanceEnabled == false)
        {
            GemCounterText.GetComponent<UnityEngine.UI.Text>().text = "" + PlayerPrefs.GetInt("PlayerGemAmount");
        }
    }

}
