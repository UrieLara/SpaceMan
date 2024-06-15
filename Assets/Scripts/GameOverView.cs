using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class GameOverView : MonoBehaviour
{
    public Text coinsCollected, scorePlayer, maxScore;
    PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.gameOver)
        {
            Result();
        }
    }

    void Result()
    {
        coinsCollected.text = GameView.sharedInstance.coinsText.text;
        scorePlayer.text = GameView.sharedInstance.scoreText.text;
        maxScore.text = "Max Score: " + PlayerPrefs.GetFloat("maxScore").ToString("f1");

    }

}
