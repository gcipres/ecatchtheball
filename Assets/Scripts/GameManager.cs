using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GameState { Start, Playing, GameOver, Share }

public class GameManager : MonoBehaviour {
    public static GameState gameState;
    
    [SerializeField] private GameObject GameOverCanvas;
    [SerializeField] private GameObject Score;
    [SerializeField] private GameObject Platform;
    [SerializeField] private GameObject MenuCanvas;
    [SerializeField] private GameObject BestScore;

    private const string BEST_SCORE = "best_score";

    void Start() {
        gameState = GameState.Start;
    }

    void Update() {
        MenuCanvas.SetActive(gameState == GameState.Start);
        Score.SetActive(gameState == GameState.Playing);
        Platform.SetActive(gameState == GameState.Playing);
        GameOverCanvas.SetActive(gameState == GameState.GameOver);

        if (gameState == GameState.GameOver) {
            int bestScore = PlayerPrefs.GetInt(BEST_SCORE, 0);

            if (bestScore < ScoreManager.score) {
                bestScore = ScoreManager.score;
                PlayerPrefs.SetInt(BEST_SCORE, bestScore);
            }

            TextMeshProUGUI bestScoreTMP = BestScore.GetComponent<TextMeshProUGUI>();
            bestScoreTMP.text = "Best score: " + bestScore.ToString();
        }
    }

    public void StartGame() {
        ScoreManager.score = 0;
        BallSpawner.spawned = false;
        gameState = GameState.Playing;

        BannerAds.HideBanner();
    }
}
