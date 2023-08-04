using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startingText;


    public static int numOfCoins;
    //public Text coinsText;
    public TextMeshProUGUI coinText;
    public TextMeshProUGUI highScoreText;
    void Start()
    {
        gameOver = false;
        Time.timeScale = 1.0f;
        isGameStarted = false;
        numOfCoins = 0;
        //PlayerPrefs.SetInt("HighScore", numOfCoins);
        UpdateHighScoreText(); 

    }

    private void OnEnable()
    {
        Coin.OnCoinCollected += HandleCoinPickUp;
    }

    private void OnDisable()
    {
        Coin.OnCoinCollected -= HandleCoinPickUp;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        //coinText.text = "Coins: " + numOfCoins;
        if (!isGameStarted && SwipeManager.tap )
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }

    void HandleCoinPickUp()
    {
        numOfCoins++;
        coinText.text = "Coins: " + numOfCoins;
        CheckHighScore();
    }

    void CheckHighScore()
    {
        if (numOfCoins > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", numOfCoins);
            UpdateHighScoreText();

        }
    }

    void UpdateHighScoreText()
    {
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt("HighScore", 0);

    }
}
