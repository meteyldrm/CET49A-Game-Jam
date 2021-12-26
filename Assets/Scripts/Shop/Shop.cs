using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public Button Black;
    public Button Green;
    public Button Blue;
    public Button Red;

    public Text BlackText;
    public Text GreenText;
    public Text BlueText;
    public Text RedText;

    public string carChoice;

    private int blackPrice = 1;
    private int greenPrice = 30;
    private int bluePrice = 50;
    private int redPrice = 100;

    public Text NotEnoughCoin;
    private float timeToDisable;
    private float timeToEnable = 2f;

    void Start()
    {
        //Initialize();
        carChoice = PlayerPrefs.GetString("CarChoice");
        InitilizeButtons();
        NotEnoughCoin.enabled = false;

        TotalCoin.SetTotalCoin();
    }

    void BuyCarSkin(string color)
    {
        switch (color)
        {
            case "black":
                if (PlayerPrefs.GetInt("Coin") < blackPrice)
                {
                    Debug.Log("Not Enough Coin");
                    ShowNotEnoughCoin();
                    break;
                }
                else
                {
                    Black.onClick.RemoveAllListeners();
                    PlayerPrefs.SetInt("HasBlack", 1);
                    BlackText.text = "USE";
                    Black.onClick.AddListener(delegate { UseCarSkin("black"); });
                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - blackPrice);
                    InitilizeButtons();
                    TotalCoin.SetTotalCoin();
                    break;
                }
            case "green":
                if (PlayerPrefs.GetInt("Coin") < greenPrice)
                {
                    Debug.Log("Not Enough Coin");
                    ShowNotEnoughCoin();
                    break;
                }
                else
                {
                    Green.onClick.RemoveAllListeners();
                    PlayerPrefs.SetInt("HasGreen", 1);
                    GreenText.text = "USE";
                    Green.onClick.AddListener(delegate { UseCarSkin("green"); });
                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - greenPrice);
                    InitilizeButtons();
                    TotalCoin.SetTotalCoin();
                    break;
                }
            case "blue":
                if (PlayerPrefs.GetInt("Coin") < bluePrice)
                {
                    Debug.Log("Not Enough Coin");
                    ShowNotEnoughCoin();
                    break;
                }
                else
                {
                    Blue.onClick.RemoveAllListeners();
                    PlayerPrefs.SetInt("HasBlue", 1);
                    BlueText.text = "USE";
                    Blue.onClick.AddListener(delegate { UseCarSkin("blue"); });
                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - bluePrice);
                    InitilizeButtons();
                    TotalCoin.SetTotalCoin();
                    break;
                }
            case "red":
                if (PlayerPrefs.GetInt("Coin") < redPrice)
                {
                    Debug.Log("Not Enough Coin");
                    ShowNotEnoughCoin();
                    break;
                }
                else
                {
                    Red.onClick.RemoveAllListeners();
                    PlayerPrefs.SetInt("HasRed", 1);
                    RedText.text = "USE";
                    Red.onClick.AddListener(delegate { UseCarSkin("red"); });
                    PlayerPrefs.SetInt("Coin", PlayerPrefs.GetInt("Coin") - redPrice);
                    InitilizeButtons();
                    TotalCoin.SetTotalCoin();
                    break;
                }
        }
    }

    void UseCarSkin(string color)
    {
        switch (color)
        {
            case "black":
                PlayerPrefs.SetString("CarChoice", "Black");
                break;
            case "green":
                PlayerPrefs.SetString("CarChoice", "Green");
                break;
            case "blue":
                PlayerPrefs.SetString("CarChoice", "Blue");
                break;
            case "red":
                PlayerPrefs.SetString("CarChoice", "Red");
                break;
        }
        InitilizeButtons();
    }

    void InitilizeButtons()
    {
        carChoice = PlayerPrefs.GetString("CarChoice");
        Black.onClick.RemoveAllListeners();
        Green.onClick.RemoveAllListeners();
        Blue.onClick.RemoveAllListeners();
        Red.onClick.RemoveAllListeners();

        if (PlayerPrefs.GetInt("HasBlack") == 1)
        {
            if (carChoice == "Black")
            {
                BlackText.text = "USED";
                Black.interactable = false;
            }
            else
            {
                BlackText.text = "USE";
                Black.onClick.AddListener(delegate { UseCarSkin("black"); });
                Black.interactable = true;
            }
        }
        else
        {
            BlackText.text = "BUY / " + blackPrice.ToString() + " COIN";
            Black.onClick.AddListener(delegate { BuyCarSkin("black"); });
        }

        if (PlayerPrefs.GetInt("HasGreen") == 1)
        {
            if (carChoice == "Green")
            {
                GreenText.text = "USED";
                Green.interactable = false;
            }
            else
            {
                GreenText.text = "USE";
                Green.onClick.AddListener(delegate { UseCarSkin("green"); });
                Green.interactable = true;
            }
        }
        else
        {
            GreenText.text = "BUY / " + greenPrice.ToString() + " COIN";
            Green.onClick.AddListener(delegate { BuyCarSkin("green"); });
        }

        if (PlayerPrefs.GetInt("HasBlue") == 1)
        {
            if (carChoice == "Blue")
            {
                BlueText.text = "USED";
                Blue.interactable = false;
            }
            else
            {
                BlueText.text = "USE";
                Blue.onClick.AddListener(delegate { UseCarSkin("blue"); });
                Blue.interactable = true;
            }
        }
        else
        {
            BlueText.text = "BUY / " + bluePrice.ToString() + " COIN";
            Blue.onClick.AddListener(delegate { BuyCarSkin("blue"); });
        }

        if (PlayerPrefs.GetInt("HasRed") == 1)
        {
            if (carChoice == "Red")
            {
                RedText.text = "USED";
                Red.interactable = false;
            }
            else
            {
                RedText.text = "USE";
                Red.onClick.AddListener(delegate { UseCarSkin("red"); });
                Red.interactable = true;
            }
        }
        else
        {
            RedText.text = "BUY / " + redPrice.ToString() + " COIN";
            Red.onClick.AddListener(delegate { BuyCarSkin("red"); });
        }
    }

    void ShowNotEnoughCoin()
    {
        NotEnoughCoin.enabled = true;
        timeToDisable = Time.time + timeToEnable;
    }

    private void Update()
    {
        if (NotEnoughCoin.enabled && (Time.time >= timeToDisable))
        {
            NotEnoughCoin.enabled = false;
        }
    }
}
