using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameController gameController;
    [SerializeField] private TextMeshProUGUI textScore;

    private void Update()
    {
        textScore.text = gameController.CurrentScore.ToString("F0");
    }
}
