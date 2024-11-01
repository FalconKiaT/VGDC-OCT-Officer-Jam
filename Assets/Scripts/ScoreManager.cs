using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public static int score = 0;
    public TextMeshProUGUI scoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        // Handle singleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE: " + score;
    }
}
