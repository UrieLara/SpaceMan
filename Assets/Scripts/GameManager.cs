using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum GameState
{
    menu,
    inGame,
    gameOver
}

public class GameManager : MonoBehaviour
{
    public GameState currentGameState = GameState.menu;

    public static GameManager sharedInstance; //singleton
    
    PlayerController controller;

    public int collectedObject = 0;


    public AudioSource inGameSound;
    public AudioSource gameOverSound;

    private void Awake()
    {
        if(sharedInstance == null)
        {
            sharedInstance = this;
        }
        
    }

    void Start()
    {
        controller = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if(Input.GetButton("Submit") && currentGameState != GameState.inGame)
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        SetGameState(GameState.inGame);
        inGameSound.Play();

    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
        gameOverSound.Play();
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }


    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            MenuManager.sharedInstance.ToggleMainMenu(true); 
            MenuManager.sharedInstance.ToggleGameMenu(false);
            MenuManager.sharedInstance.ToggleGameOverMenu(false);

        }
        else if (newGameState == GameState.inGame)
        {
            controller.StartGame();
            LevelManager.sharedInstance.RemoveAllLevelBlock();
            LevelManager.sharedInstance.GenerateInitialBlock();
            
            MenuManager.sharedInstance.ToggleMainMenu(false); 
            MenuManager.sharedInstance.ToggleGameMenu(true); 
            MenuManager.sharedInstance.ToggleGameOverMenu(false);
        }
        else if (newGameState == GameState.gameOver)
        {
            MenuManager.sharedInstance.ToggleMainMenu(true);
            MenuManager.sharedInstance.ToggleGameMenu(false); 
            MenuManager.sharedInstance.ToggleGameOverMenu(true);

            collectedObject = 0;
        }

        this.currentGameState = newGameState;
    }

    public void CollectObject(Collectable collectable)
    {
        collectedObject += collectable.value;
    }

    /*NOTA
     
     Se realiza un singleton para asegurarse de que el GameManager es el �nico. 
    El GameManager ser� compartido y debe ser est�tico para evitar cambios desde otros scripts. */
}
