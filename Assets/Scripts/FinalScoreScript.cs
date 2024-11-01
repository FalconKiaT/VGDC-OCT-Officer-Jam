using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScoreScript : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text += ScoreManager.score;
    }
}
