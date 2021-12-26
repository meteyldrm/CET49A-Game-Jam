using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartGame : MonoBehaviour
{
    public Button button;

    private void Start()
    {
        button.onClick.AddListener(GameStart);
    }

    void GameStart()
    {
        SceneManager.LoadScene("GameScene");
    }
}
