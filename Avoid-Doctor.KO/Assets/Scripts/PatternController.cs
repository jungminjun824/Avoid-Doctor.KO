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
        //�����ϰ��ִ� 
        patternIndexs = new int[patterns.Length];

        for (int i = 0; i < patternIndexs.Length; i++)
        {
            patternIndexs[i] = i;
        }
    }

    private void Update()
    {
        if (gameController.IsGamePlay == false) return;

        // ���� ������� ������ ����Ǿ� ������Ʈ�� ��Ȱ��ȭ �Ǹ�
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
