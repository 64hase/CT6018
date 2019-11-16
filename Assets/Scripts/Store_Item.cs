using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store_Item : MonoBehaviour
{
    [SerializeField] private MeshFilter PlayerHatMeshFilter;
    [SerializeField] private int StoreItemNumber;
    [SerializeField] private int HatPrice;
    [SerializeField] private Mesh HatMesh;
    [SerializeField] private Button PurchaseButton;
    [SerializeField] private Text ButtonText;
    [SerializeField] private Sprite HatSprite;
    [SerializeField] private Image StoreImage;
    [SerializeField] private bool ResetOwnership = false;
    [SerializeField] private MeshFilter[] HatArray;
    public bool CanAfford
    {
        get { return PlayerPrefs.GetInt("PlayerCoinAmount", 0) >= HatPrice; }
        //private set { }
    }

    private void Start()
    {
        string PlayerPrefValue = ("Hat" + StoreItemNumber + "_Own");
        PurchaseButton.onClick.AddListener(OnClick);
        StoreImage.sprite = HatSprite;
        ButtonText.GetComponent<Text>().text = HatPrice.ToString();

        if(ResetOwnership == true)
        {
            PlayerPrefs.SetInt(PlayerPrefValue, 0);
        }
    }
    private void OnClick()
    {
        string PlayerPrefValue = ("Hat" + StoreItemNumber + "_Own");
        if (PlayerPrefs.GetInt(PlayerPrefValue, 0) == 1)
        {
            for (int i = 0; i < HatArray.Length; i++)
            {
                HatArray[i].mesh = HatMesh;
            }
        }
        else
        {
            if (PlayerPrefs.GetInt("PlayerCoinAmount", 0) >= HatPrice)
            {
                int coinAmount = PlayerPrefs.GetInt("PlayerCoinAmount");

                PlayerPrefs.SetInt("PlayerCoinAmount", coinAmount - HatPrice);
                PlayerPrefs.SetInt(PlayerPrefValue, 1);

                ButtonText.text = "Select";
                for (int i = 0; i < HatArray.Length; i++)
                {
                    HatArray[i].mesh = HatMesh;
                }
            }
            else
            {
                //what happens if the player can't afford the hat?
                LeanTween.moveLocalX(PurchaseButton.gameObject, 10, 0.1F);
                LeanTween.moveLocalX(PurchaseButton.gameObject, -10, 0.1F).setDelay(0.11F);
                LeanTween.moveLocalX(PurchaseButton.gameObject, 10, 0.1F).setDelay(0.21F);
                LeanTween.moveLocalX(PurchaseButton.gameObject, -10, 0.1F).setDelay(0.31F);
                LeanTween.moveLocalX(PurchaseButton.gameObject, 0, 0.1F).setDelay(0.41F);
            }
        }
    }
    private void Update()
    {
       /* if (PlayerPrefs.GetInt("PlayerCoinAmount", 0) >= HatPrice)
        {
            CanAfford = true;
        }
        else
        {
            CanAfford = false;
        }*/
    }
}
