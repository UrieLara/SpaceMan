using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    // Update is called once per frame
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
    }

    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }

    void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {
            //TODO: mostrar el menu
        }
        else if(newGameState == GameState.inGame)
        {
            //TODO: preparar escena de juego
            controller.StartGame();
        }
        else if(newGameState==GameState.gameOver)
        {
            //TODO: preparar fin de partida
        }

        this.currentGameState = newGameState;
    }

    /*NOTA
     
     Se realiza un singleton para asegurarse de que el GameManager es el único. 
    El GameManager será compartido y debe ser estático para evitar cambios desde otros scripts. */
}
