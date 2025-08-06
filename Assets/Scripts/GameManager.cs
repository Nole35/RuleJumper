using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;


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
    public TextMeshProUGUI highScoreText;
    public GameObject goText;


    [Header("Victory Effects")]
    public GameObject victoryConfettiPrefab;

    private const string HighScoreKey = "HighScore";
    private const string HighTimeKey = "HighTime";

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        if (goText != null)
            StartCoroutine(HideGoText());
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

        // 🎉 Konfete efekat – SAMO ako je victory
        if (victory && victoryConfettiPrefab != null)
        {
            GameObject confetti = Instantiate(
                victoryConfettiPrefab,
                Camera.main.transform.position + new Vector3(0, 0, 5),
                Quaternion.identity
            );
            Destroy(confetti, 3f); // konfete traju 3 sekunde
        }

        // 🔒 High score update
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

    private IEnumerator HideGoText()
    {
        yield return new WaitForSeconds(1f);
        if (goText != null)
            goText.SetActive(false);
    }
}
