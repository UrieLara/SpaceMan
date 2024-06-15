using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager sharedInstance;
    public Canvas menuMainCanvas, menuGameCanvas, menuGameOverCanvas;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    void Start()
    {
        menuGameCanvas.enabled = false;
        menuGameOverCanvas.enabled = false;
    }

    public void ToggleMainMenu(bool show)
    {
        menuMainCanvas.enabled = show;
    }

    public void ToggleGameMenu(bool show)
    {
        menuGameCanvas.enabled = show;
    }

    public void ToggleGameOverMenu(bool show)
    {
        menuGameOverCanvas.enabled = show;
    }

    public void ExitGame()
    {
        //if regionales
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
