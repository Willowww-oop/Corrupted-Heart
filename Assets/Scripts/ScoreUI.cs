using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    void Start()
    {
        UpdateUI(Score.Instance.score);
    }

    void OnEnable()
    {
        Score.OnScoreChanged += UpdateUI;
    }

    void OnDisable()
    {
        Score.OnScoreChanged += UpdateUI;
    }

    private void UpdateUI(int newScore)
    {
        scoreText.text = "Score: " + newScore;
    }
}
