﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Store_V02_Item : MonoBehaviour
{
    [SerializeField] private Image StoreItemImage;
    [SerializeField] private Text StoreItemPrice;
    [SerializeField] private Button PurchaseButton;
    [SerializeField] private GameObject[] PlayerHatPlaceholders;
    private Store_V02 Store_V02;
    private bool OwnsHatBool = false;
    private Mesh StoreItemMesh;
    private int Price;
    private int PlayerCoins
    {
        get { return PlayerPrefs.GetInt("PlayerCoinAmount"); }
        set { PlayerPrefs.SetInt("PlayerCoinAmount", value); }
    }
    private void Start()
    {
        OnSetButtonState();
        StoreItemImage.GetComponent<Button>().onClick.AddListener(OnImagePress);
        Store_V02 = EventSystem.current.GetComponent<Store_V02>();
        PlayerHatPlaceholders = Store_V02.PlayerTreeHat;
    }
    public void OnSetStoreData(Sprite HatSprite, Mesh HatMesh, int HatPrice, bool OwnsHat)
    {
        OwnsHatBool = OwnsHat;
        Price = HatPrice;
        StoreItemImage.sprite = HatSprite;
        StoreItemMesh = HatMesh;
        if (OwnsHat == true)
        { StoreItemPrice.text = "Select"; }
        else
        { StoreItemPrice.text = HatPrice.ToString(); }
        PurchaseButton.onClick.AddListener(OnStoreButtonPress);

    }
    private void OnImagePress()
    {
        Store_V02.ResetHat = true;
        for (int i = 0; i < PlayerHatPlaceholders.Length; i++)
        {
            //If so, set all hat meshes to this hat
            PlayerHatPlaceholders[i].GetComponent<MeshFilter>().mesh = StoreItemMesh;
        }
    }
    private void OnSetButtonState()
    {
        if (PlayerCoins < Price)
        {
            PurchaseButton.interactable = false;
        }
        else
        {
            PurchaseButton.interactable = true;
        }

    }
    private void OnStoreButtonPress()
    {
        //Checks if the hat is owned by the player
        if (OwnsHatBool == true)
        {
            for(int i = 0; i < PlayerHatPlaceholders.Length; i++)
            {
                //If so, set all hat meshes to this hat
                PlayerHatPlaceholders[i].GetComponent<MeshFilter>().mesh = StoreItemMesh;
            }
        }
        else
        //If not owned, either purchase the hat if the player has enough coins or shake to indicate that this item cannot be afforded
        {
            if (PlayerCoins >= int.Parse(StoreItemPrice.ToString()))
            {
                //Button animation here
                PlayerCoins = PlayerCoins - int.Parse(StoreItemPrice.ToString());
                OwnsHatBool = true;
                PlayerPrefs.SetInt("OwnsHat_" + StoreItemMesh.name, 1);
                OnStoreButtonPress();
            }
            else
            {
                LeanTween.moveLocalX(PurchaseButton.gameObject, 10, 0.1F);
                LeanTween.moveLocalX(PurchaseButton.gameObject, -10, 0.1F).setDelay(0.11F);
                LeanTween.moveLocalX(PurchaseButton.gameObject, 10, 0.1F).setDelay(0.21F);
                LeanTween.moveLocalX(PurchaseButton.gameObject, -10, 0.1F).setDelay(0.31F);
                LeanTween.moveLocalX(PurchaseButton.gameObject, 0, 0.1F).setDelay(0.41F);
            }
        }
    }
}
