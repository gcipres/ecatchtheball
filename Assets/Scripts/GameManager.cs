using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameState { Start, Tutorial, Playing, GameOver }

public class GameManager : MonoBehaviour {
    public static GameState gameState;
    public static int score;
    
    [SerializeField] private GameObject GameOverCanvas;
    [SerializeField] private GameObject Score;
    [SerializeField] private GameObject ScoreGameOver;
    [SerializeField] private GameObject BestScore;
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject HowToPlay;
    
    private const string BEST_SCORE = "best_score";

    private bool movementChanged;

    void Start() {
        movementChanged = false;
        gameState = GameState.Start;
    }

    void Update() {
        MenuCanvas.SetActive(gameState == GameState.Start);
        HowToPlay.SetActive(gameState == GameState.Tutorial);
        Platform.SetActive(gameState == GameState.Tutorial || gameState == GameState.Playing);
        Score.SetActive(gameState == GameState.Playing);
        GameOverCanvas.SetActive(gameState == GameState.GameOver);

        UpdateScore();
        GetBestScore();
    }

    public void StartGame() {
        score = 0;
        BallSpawner.spawned = false;
        int tutorialShowed = PlayerPrefs.GetInt("tutorial", 0);
        gameState = tutorialShowed == 0 ? GameState.Tutorial : GameState.Playing;
        PlayerPrefs.SetInt("tutorial", 1);

        BannerAds.HideBanner();
    }

    private void UpdateScore() {
        if (BallSpawner.movementSpeed < 1500f && GameManager.score > 0 && GameManager.score % 2 == 0) {
            if (!movementChanged) {
                BallSpawner.movementSpeed += 99f;
                movementChanged = true;
            }
        } else 
            movementChanged = false;
        
        TextMeshProUGUI scoreTMP = Score.GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI scoreGameOverTMP = ScoreGameOver.GetComponent<TextMeshProUGUI>();
        scoreTMP.text = score.ToString();
        scoreGameOverTMP.text = score.ToString();
    }

    private void GetBestScore() {
        if (gameState == GameState.GameOver) {
            int bestScore = PlayerPrefs.GetInt(BEST_SCORE, 0);

            if (bestScore < score) {
                bestScore = score;
                PlayerPrefs.SetInt(BEST_SCORE, bestScore);
            }

            TextMeshProUGUI bestScoreTMP = BestScore.GetComponent<TextMeshProUGUI>();
            bestScoreTMP.text = "Best score: " + bestScore.ToString();
        }
    }
}
