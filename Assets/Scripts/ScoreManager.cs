using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    public static int score;

    private bool movementChanged;

    void Start() {
        score = 0;
        movementChanged = false;
    }

    void Update() {
        if (BallSpawner.movementSpeed < 1500f && score > 0 && score % 2 == 0) {
            if (!movementChanged) {
                BallSpawner.movementSpeed += 99f;
                movementChanged = true;
            }
        } else 
            movementChanged = false;
        
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = score.ToString();
    }
}
