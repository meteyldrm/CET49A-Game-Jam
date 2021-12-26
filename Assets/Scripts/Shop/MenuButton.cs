using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public Button button;

    void Start()
    {
        button.onClick.AddListener(BackMenu);
    }

    void BackMenu()
    {
        SceneManager.LoadScene("GameScene");
    }
}
