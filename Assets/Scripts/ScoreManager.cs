using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
    private int score;

    void Start() {
        score = 0;
    }

    public void AddOnePoint() {
        score++;

        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
        tmp.text = score.ToString();
    }
}
