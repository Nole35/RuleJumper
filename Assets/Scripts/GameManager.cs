using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int score = 0;
    private float timer = 0f;
    private bool isGameEnded = false;

    [Header("UI References")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject endScreen;
    public TextMeshProUGUI endMessageText;
    public TextMeshProUGUI finalScoreText;

    [Header("High Score")]
    public TextMeshProUGUI highScoreText;

    private const string HighScoreKey = "HighScore";
    private const string HighTimeKey = "HighTime";

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (isGameEnded) return;

        timer += Time.deltaTime;
        timerText.text = $"Time: {timer:F1}s";
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = $"Score: {score}";
    }

    public void EndGame(bool victory)
    {
        isGameEnded = true;
        endScreen.SetActive(true);

        endMessageText.text = victory ? "Victory!" : "Game Over";
        finalScoreText.text = $"Score: {score} | Time: {timer:F1}s";

        // Provera i ažuriranje najboljeg rezultata
        int savedHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);
        float savedHighTime = PlayerPrefs.GetFloat(HighTimeKey, float.MaxValue);

        bool isNewHigh = false;

        if (score > savedHighScore || (score == savedHighScore && timer < savedHighTime))
        {
            PlayerPrefs.SetInt(HighScoreKey, score);
            PlayerPrefs.SetFloat(HighTimeKey, timer);
            isNewHigh = true;
        }

        string bestText = isNewHigh ? "NEW HIGH SCORE!\n" : "Best:\n";
        highScoreText.text = bestText + $"Score: {PlayerPrefs.GetInt(HighScoreKey)} | Time: {PlayerPrefs.GetFloat(HighTimeKey):F1}s";
    }

    public bool IsGameEnded()
    {
        return isGameEnded;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

