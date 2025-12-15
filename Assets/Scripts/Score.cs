using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score Instance { get; private set; }

    public int score;

    public static event System.Action<int> OnScoreChanged;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;

        OnScoreChanged?.Invoke(score);
    }
}
