using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    private int score;
    private bool movementChanged;

    void Start() {
        score = 0;
        movementChanged = false;
    }

    void Update() {
        if (score > 0 && score % 5 == 0) {
            if (!movementChanged) {
                BallSpawner.movementSpeedX *= 1.15f;
                BallSpawner.movementSpeedY *= 1.15f;
                movementChanged = true;
            }
        } else 
            movementChanged = false;
    }

    public void AddOnePoint() {
        score++;

        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = score.ToString();
    }
}
