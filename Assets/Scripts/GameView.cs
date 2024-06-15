using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{

    public static GameView sharedInstance;

    public Text scoreText, coinsText, maxScoreText;
    PlayerController playerController;


    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }

    }

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            int coins = GameManager.sharedInstance.collectedObject;
            float score = playerController.GetTravelledDistance();
            float maxScore = PlayerPrefs.GetFloat("maxScore",0f);

            coinsText.text = coins.ToString();
            scoreText.text = "Score: " + score.ToString("f1"); //float de 1 decimal
            maxScoreText.text = "Max Score: " + maxScore.ToString("f1");
        }

    }
}
