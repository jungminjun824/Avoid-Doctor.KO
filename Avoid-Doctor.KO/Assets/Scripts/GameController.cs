using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private UIController uiController;
    //[SerializeField] private GameObject pattern01;
    [SerializeField] private PatternController patternController;

    private readonly float scoreScale = 20;

    public float CurrentScore { private set; get; } = 0;

    public bool IsGamePlay { private set; get; } = false;

    public void GameStart()
    {
        uiController.GameStart();
        patternController.GameStart();
        IsGamePlay = true;
    }

    public void GameExit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    public void GameOver()
    {
        uiController.GameOver();
        patternController.GameOver();
        IsGamePlay = false;
    }

    private void Update()
    {
        if (IsGamePlay == false) return;

        CurrentScore += Time.deltaTime * scoreScale;
    }
}