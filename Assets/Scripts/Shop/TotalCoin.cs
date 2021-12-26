using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TotalCoin : MonoBehaviour
{
    public static GameObject Total;
    void Start()
    {
        Total = gameObject;
        SetTotalCoin();
    }

    public static void SetTotalCoin()
    {
        Total.GetComponent<Text>().text = "TOTAL COIN: " + PlayerPrefs.GetInt("Coin").ToString();
    }
}
