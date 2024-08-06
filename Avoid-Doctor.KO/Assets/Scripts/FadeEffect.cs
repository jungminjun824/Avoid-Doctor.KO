using System.Collections;
using UnityEngine;
using TMPro;

public class FadeEffect : MonoBehaviour
{
    [SerializeField][Range(0.01f, 10f)] private float effectTime;

    private TextMeshProUGUI effectText;

    private void Awake()
    {
        effectText = GetComponent<TextMeshProUGUI>();
    }

    public void FadeIn()
    {
        StartCoroutine(Fade(0,1));
    }
    public void FadeOut()
    {
        StartCoroutine(Fade(1, 0));
    }
    private IEnumerator Fade(float start, float end)
    {
        float current = 0;
        float percent = 0;

        while ( percent < 1)
        {
            current += Time.deltaTime;
            percent = current / effectTime;

            Color color = effectText.color;
            color.a = Mathf.Lerp(start, end, percent);
            effectText.color = color;

            yield return null; 
        }
    }
}
