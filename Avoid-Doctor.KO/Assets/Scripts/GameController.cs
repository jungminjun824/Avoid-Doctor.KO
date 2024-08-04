using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private readonly float scoreScale = 20;

    public float CurrentScore { private set; get; } = 0;

    private void Update()
    {
        CurrentScore += Time.deltaTime * scoreScale;
    }
}
