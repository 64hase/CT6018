using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin_Counter_Logic : MonoBehaviour
{
    [SerializeField]
    private int CoinAmount;
    private Text CoinCounterText;
    public bool ResetOnStartup;
    // Start is called before the first frame update
    private void Start()
    {
        CoinCounterText = this.GetComponentInChildren<Text>();
        PlayerPrefs.GetInt("PlayerCoinAmount", 0);
        if (ResetOnStartup == true)
        {
            PlayerPrefs.SetInt("PlayerCoinAmount", 0);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        CoinCounterText.GetComponent<UnityEngine.UI.Text>().text = "" + PlayerPrefs.GetInt("PlayerCoinAmount");
    }

}
