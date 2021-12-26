using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopButton : MonoBehaviour
{
    public Button button;

    void Start()
    {
        button.onClick.AddListener(Shop);
    }

    void Shop()
    {
        SceneManager.LoadScene("Shop");
    }
}
