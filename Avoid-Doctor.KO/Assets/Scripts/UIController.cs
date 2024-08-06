using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    [Header("Main UI")]
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private TextMeshProUGUI textMainGrade;

    [Header("Game UI")]
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private TextMeshProUGUI textScore;

    [Header("Result UI")]
    [SerializeField] private GameObject resultPanel;
    [SerializeField] TextMeshProUGUI textResultScore;
    [SerializeField] TextMeshProUGUI textResultGrade;
    [SerializeField] TextMeshProUGUI textResultTalk;
    [SerializeField] TextMeshProUGUI textResultHighScore;

    [Header("Result UI Animation")]
    [SerializeField] private ScaleEffect effectGameOver;
    [SerializeField] private CountingEffect effectResultScore;
    [SerializeField] FadeEffect effectResultGrade;

    private void Awake()
    {
        textMainGrade.text = PlayerPrefs.GetString("HIGHGRADE");
    }

    public void GameStart()
    {
        mainPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void GameOver()
    {
        int currentScore = (int)gameController.CurrentScore;

        CalculateGradeAndTalk(currentScore);
        CalculateHighScore(currentScore);

        gamePanel.SetActive(false);
        resultPanel.SetActive(true);

        effectGameOver.Play(500, 200);
        effectResultScore.Play(0, currentScore, effectResultGrade.FadeIn);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToYoutube()
    {
        Application.OpenURL("https://www.yuotube.com/@unitynote");
    }

    private void Update()
    {
        textScore.text = gameController.CurrentScore.ToString("F0");
    }

    private void CalculateGradeAndTalk(int score)
    {
        if(score < 2000)
        {
            textResultGrade.text = "F";
            textResultTalk.text = "�� ��\n����غ��ô�!";
        }
        else if(score < 3000)
        {
            textResultGrade.text = "D";
            textResultTalk.text = "��������!";
        }
        else if (score < 4000)
        {
            textResultGrade.text = "C";
            textResultTalk.text = "�����ϴ� �����\n���Դϴ�!";
        }
        else if (score < 5000)
        {
            textResultGrade.text = "B";
            textResultTalk.text = "A�� ����\n�ʾҽ��ϴ�!";
        }

        else
        {
            textResultGrade.text = "A";
            textResultTalk.text = "����Ƽ��\n�������ϴ�\n�׳�����!";
        }
    }

    private void CalculateHighScore(int score)
    {
        int highScore = PlayerPrefs.GetInt("HIGHSCORE");

        if(score > highScore)
        {
            PlayerPrefs.SetString("HIGHGRADE", textResultGrade.text);
            PlayerPrefs.SetInt("HIGHSCORE", score);
            textResultHighScore.text = score.ToString();
        }
        else
        {
            textResultHighScore.text = highScore.ToString();
        }
    }
}
