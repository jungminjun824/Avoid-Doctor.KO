using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField] private GameObject[] imageHP;
    private int currentHP;

    [SerializeField] private float invincibilityDuration;
    private bool isInvincibility = false;

    private SoundController soundController;
    private SpriteRenderer spriteRenderer;

    private Color originColor;

    private void Awake()
    {
        soundController = GetComponentInChildren<SoundController>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        currentHP = imageHP.Length;
        originColor = spriteRenderer.color;
    }

    public bool TakeDamage()
    {
        if (isInvincibility == true) return false;

        if (currentHP > 1)
        {
            soundController.Play(0);
            StartCoroutine(nameof(OnInvincibility));

            currentHP--;
            imageHP[currentHP].SetActive(false);
        }
        else
        {
            return true;
        }

        return false;
    }

    private IEnumerator OnInvincibility()
    {
        isInvincibility = true;

        float current = 0;
        float percent = 0;
        float colorSpeed = 10;

        while(percent < 1)
        {
            current += Time.deltaTime;
            percent = current / invincibilityDuration;

            spriteRenderer.color = Color.Lerp(originColor, Color.red, Mathf.PingPong(Time.time * colorSpeed, 1));

            yield return null;
        }

        spriteRenderer.color = originColor;
        isInvincibility = false;
    }
}
