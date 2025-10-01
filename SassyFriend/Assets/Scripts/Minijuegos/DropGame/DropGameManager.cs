using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DropGameManager : MonoBehaviour
{
    public static DropGameManager Instance;

    [Header("Config")]
    public int lives = 3;
    public float gameDuration = 60f;
    public int scorePerClean = 10;
    public int penaltyDirty = 5;

    [Header("UI")]
    public Text scoreText;
    public Text livesText; //pérame aún no lo hago
    public Text timerText;

    [Header("State")]
    private float timer;
    private int score;

    private void Awake() => Instance = this;

    private void Start()
    {
        timer = gameDuration;
        score = 0;
        UpdateUI();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 || lives <= 0)
        {
            EndGame();
        }

        UpdateUI();
    }

    public void CatchCleanDrop()
    {
        score += scorePerClean;
    }

    public void CatchDirtyDrop()
    {
        lives--;
        score -= penaltyDirty;
        if (score < 0) score = 0;
    }

    private void EndGame()
    {
        Debug.Log("Juego terminado. Score final: " + score);

        FindObjectOfType<DropSpawner>().StopSpawning();


    }

    private void UpdateUI()
    {
        if (scoreText) scoreText.text = "Score: " + score;
        if (livesText) livesText.text = "Vidas: " + lives;
        if (timerText) timerText.text = "Tiempo: " + Mathf.Ceil(timer);
    }

    // por si otra clase necesita leer el estado (Que no creo)
    public int GetScore() => score;
    public float GetTimer() => timer;
    public int GetLives() => lives;
}
