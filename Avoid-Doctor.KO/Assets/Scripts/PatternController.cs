using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternController : MonoBehaviour
{
    [SerializeField] GameController gameController;
    [SerializeField] GameObject[] patterns;
    private GameObject currentPattern;
    private int[] patternIndexs;
    private int current = 0;

    private void Awake()
    {
        //보유하고있는 
        patternIndexs = new int[patterns.Length];

        for (int i = 0; i < patternIndexs.Length; i++)
        {
            patternIndexs[i] = i;
        }
    }

    private void Update()
    {
        if (gameController.IsGamePlay == false) return;

        // 현재 재생중인 패턴이 종료되어 오브젝트가 비활성화 되면
        if(currentPattern.activeSelf == false)
        {
            ChangePattern();
        }
    }

    public void GameStart()
    {
        ChangePattern();
    }

    public void GameOver()
    {
        currentPattern.SetActive(false);
    }

    public void ChangePattern()
    {
        currentPattern = patterns[patternIndexs[current]];
        currentPattern.SetActive(true);
        current++;

        if(current >= patternIndexs.Length)
        {
            patternIndexs = Utils.RandomNumbers(patternIndexs.Length, patternIndexs.Length);
            current = 0;
        }
    }
}
