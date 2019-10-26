using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store_Item : MonoBehaviour
{
    private int Hat01_Own;
    private int Hat01_Price;
    public Sprite Hat01_Image;
    public Mesh Hat01;

    private int Hat02_Own;
    private int Hat02_Price;
    public Sprite Hat02_Image;
    public Mesh Hat02;

    private int Hat03_Own;
    private int Hat03_Price;
    public Sprite Hat03_Image;
    public Mesh Hat03;

    private int Hat04_Own;
    private int Hat04_Price;
    public Sprite Hat04_Image;
    public Mesh Hat04;

    private int Hat05_Own;
    private int Hat05_Price;
    public Sprite Hat05_Image;
    public Mesh Hat05;

    private int Hat06_Own;
    private int Hat06_Price;
    public Sprite Hat06_Image;
    public Mesh Hat06;

    public int Button_Number;
    private GameObject Hat;
    private Button PurchaseItemButton;
   // public Color SelectNormalColour;
    //public Color HighlightNormalColour;
    //public Color Pressed;
    private int PlayerCoinValue;

    // Start is called before the first frame update
    private void Start()
    {
        Hat01_Own = PlayerPrefs.GetInt("Hat01_Own");
        Hat01_Price = 50;
        if (Button_Number == 1)
        {
            this.GetComponentInChildren<Image>().overrideSprite = Hat01_Image;
        }

        Hat02_Own = PlayerPrefs.GetInt("Hat02_Own");
        Hat02_Price = 125;
        if (Button_Number == 1)
        {
            this.GetComponentInChildren<Image>().overrideSprite = Hat01_Image;
        }

        Hat03_Own = PlayerPrefs.GetInt("Hat03_Own");
        Hat03_Price = 200;
        if (Button_Number == 1)
        {
            this.GetComponentInChildren<Image>().overrideSprite = Hat01_Image;
        }

        Hat04_Own = PlayerPrefs.GetInt("Hat04_Own");
        Hat04_Price = 250;
        if (Button_Number == 1)
        {
            this.GetComponentInChildren<Image>().overrideSprite = Hat01_Image;
        }

        Hat05_Own = PlayerPrefs.GetInt("Hat05_Own");
        Hat05_Price = 375;
        if (Button_Number == 1)
        {
            this.GetComponentInChildren<Image>().overrideSprite = Hat01_Image;
        }

        Hat06_Own = PlayerPrefs.GetInt("Hat06_Own");
        Hat06_Price = 525;
        if (Button_Number == 1)
        {
            this.GetComponentInChildren<Image>().overrideSprite = Hat01_Image;
        }

        PurchaseItemButton = this.GetComponentInChildren<Button>();
        PurchaseItemButton.onClick.AddListener(OnClick);

        PlayerCoinValue = PlayerPrefs.GetInt("PlayerCoinAmount");
    }

    private void Update()
    {
        //If the item is owned, change the button text to select as the button will be used to apply the hat
        if (Hat01_Own == 1)
        {
            PurchaseItemButton.GetComponentInChildren<Text>().text = "Select";
        }

        //If the item is owned, change the button text to select as the button will be used to apply the hat
        if (Hat02_Own == 1)
        {
            PurchaseItemButton.GetComponentInChildren<Text>().text = "Select";
        }

        //If the item is owned, change the button text to select as the button will be used to apply the hat
        if (Hat03_Own == 1)
        {
            PurchaseItemButton.GetComponentInChildren<Text>().text = "Select";
        }

        //If the item is owned, change the button text to select as the button will be used to apply the hat
        if (Hat04_Own == 1)
        {
            PurchaseItemButton.GetComponentInChildren<Text>().text = "Select";
        }

        //If the item is owned, change the button text to select as the button will be used to apply the hat
        if (Hat05_Own == 1)
        {
            PurchaseItemButton.GetComponentInChildren<Text>().text = "Select";
        }

        //If the item is owned, change the button text to select as the button will be used to apply the hat
        if (Hat06_Own == 1)
        {
            PurchaseItemButton.GetComponentInChildren<Text>().text = "Select";
        }
    }

    // Update is called once per frame
    private void OnClick()
    {

        //  ================[ HAT 01 ITEM LOGIC ]================

        if (Button_Number == 1)
        {
         //Does player own the hat?
         if (Hat01_Own == 1)
            {
                //Set Hat to this Hat mesh
                Hat.GetComponent<MeshFilter>().mesh = Hat01;
            }
            else
            //Player must purchase the hat
            {
                //Can player afford the price of the hat?
                if (PlayerCoinValue >= Hat01_Price)
                {
                    //Deduct the price of the hat from the player's coins
                    PlayerPrefs.SetInt("PlayerCoinAmount", PlayerCoinValue - Hat01_Price);
                    //Set Hat Mesh to this hat
                    Hat.GetComponent<MeshFilter>().mesh = Hat01;
                }
                //If player cannot afford the item...
                else
                {
                    //Print message to console stating that player cannot afford item
                    Debug.Log("Player cannot afford item");  //Needs to include message that will be visible to the player
                }
            }
        }

        //  ================[ HAT 02 ITEM LOGIC ]================

        if (Button_Number == 2)
        {
            //Does player own the hat?
            if (Hat02_Own == 1)
            {
                //Set Hat to this Hat mesh
                Hat.GetComponent<MeshFilter>().mesh = Hat02;
            }
            else
            //Player must purchase the hat
            {
                //Can player afford the price of the hat?
                if (PlayerCoinValue >= Hat02_Price)
                {
                    //Deduct the price of the hat from the player's coins
                    PlayerPrefs.SetInt("PlayerCoinAmount", PlayerCoinValue - Hat02_Price);
                    //Set Hat Mesh to this hat
                    Hat.GetComponent<MeshFilter>().mesh = Hat02;
                }
                //If player cannot afford the item...
                else
                {
                    //Print message to console stating that player cannot afford item
                    Debug.Log("Player cannot afford item");  //Needs to include message that will be visible to the player
                }
            }
        }

        //  ================[ HAT 03 ITEM LOGIC ]================

        if (Button_Number == 3)
        {
            //Does player own the hat?
            if (Hat03_Own == 1)
            {
                //Set Hat to this Hat mesh
                Hat.GetComponent<MeshFilter>().mesh = Hat03;
            }
            else
            //Player must purchase the hat
            {
                //Can player afford the price of the hat?
                if (PlayerCoinValue >= Hat03_Price)
                {
                    //Deduct the price of the hat from the player's coins
                    PlayerPrefs.SetInt("PlayerCoinAmount", PlayerCoinValue - Hat03_Price);
                    //Set Hat Mesh to this hat
                    Hat.GetComponent<MeshFilter>().mesh = Hat03;
                }
                //If player cannot afford the item...
                else
                {
                    //Print message to console stating that player cannot afford item
                    Debug.Log("Player cannot afford item");  //Needs to include message that will be visible to the player
                }
            }
        }

        //  ================[ HAT 04 ITEM LOGIC ]================

        if (Button_Number == 4)
        {
            //Does player own the hat?
            if (Hat04_Own == 1)
            {
                //Set Hat to this Hat mesh
                Hat.GetComponent<MeshFilter>().mesh = Hat04;
            }
            else
            //Player must purchase the hat
            {
                //Can player afford the price of the hat?
                if (PlayerCoinValue >= Hat04_Price)
                {
                    //Deduct the price of the hat from the player's coins
                    PlayerPrefs.SetInt("PlayerCoinAmount", PlayerCoinValue - Hat04_Price);
                    //Set Hat Mesh to this hat
                    Hat.GetComponent<MeshFilter>().mesh = Hat04;
                }
                //If player cannot afford the item...
                else
                {
                    //Print message to console stating that player cannot afford item
                    Debug.Log("Player cannot afford item");  //Needs to include message that will be visible to the player
                }
            }
        }

        //  ================[ HAT 05 ITEM LOGIC ]================

        if (Button_Number == 5)
        {
            //Does player own the hat?
            if (Hat05_Own == 1)
            {
                //Set Hat to this Hat mesh
                Hat.GetComponent<MeshFilter>().mesh = Hat05;
            }
            else
            //Player must purchase the hat
            {
                //Can player afford the price of the hat?
                if (PlayerCoinValue >= Hat05_Price)
                {
                    //Deduct the price of the hat from the player's coins
                    PlayerPrefs.SetInt("PlayerCoinAmount", PlayerCoinValue - Hat05_Price);
                    //Set Hat Mesh to this hat
                    Hat.GetComponent<MeshFilter>().mesh = Hat05;
                }
                //If player cannot afford the item...
                else
                {
                    //Print message to console stating that player cannot afford item
                    Debug.Log("Player cannot afford item");  //Needs to include message that will be visible to the player
                }
            }
        }

        //  ================[ HAT 06 ITEM LOGIC ]================

        if (Button_Number == 6)
        {
            //Does player own the hat?
            if (Hat06_Own == 1)
            {
                //Set Hat to this Hat mesh
                Hat.GetComponent<MeshFilter>().mesh = Hat06;
            }
            else
            //Player must purchase the hat
            {
                //Can player afford the price of the hat?
                if (PlayerCoinValue >= Hat06_Price)
                {
                    //Deduct the price of the hat from the player's coins
                    PlayerPrefs.SetInt("PlayerCoinAmount", PlayerCoinValue - Hat06_Price);
                    //Set Hat Mesh to this hat
                    Hat.GetComponent<MeshFilter>().mesh = Hat06;
                }
                //If player cannot afford the item...
                else
                {
                    //Print message to console stating that player cannot afford item
                    Debug.Log("Player cannot afford item");  //Needs to include message that will be visible to the player
                }
            }
        }
    }
}
