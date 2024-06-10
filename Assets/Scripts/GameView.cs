using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameView : MonoBehaviour
{
    public Text scoreText, coinsText, maxScoreText;
    PlayerController playerController;

    
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    
    void Update()
    {
        if(GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            int coins = GameManager.sharedInstance.collectedObject;
            float score = playerController.GetTravelledDistance(), maxScore = 0;
            float maxscore = PlayerPrefs.GetFloat("maxscore", 0f);

            coinsText.text = coins.ToString();
            scoreText.text = "Score: " + score.ToString("f1"); //float de 1 decimal
            maxScoreText.text = "Max Score: " + maxScore.ToString("f1");
        }
    }
}
