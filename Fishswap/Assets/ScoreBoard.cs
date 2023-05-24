using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] TMP_Text player1Score;
    [SerializeField] TMP_Text player2Score;
    public int score1;
    public int score2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreasePlayer1Score(int amount)
    {
        score1 += amount;
        UpdateScoreBoard(score1,score2);
    }
    public void IncreasePlayer2Score(int amount)
    {
        score2 += amount;
        UpdateScoreBoard(score1, score2);
    }

    public void UpdateScoreBoard(int playerscore1, int playerscore2)
    {
        SetPlayerOneScore(playerscore1);
        SetPlayerTwoScore(playerscore2);
    }

    public void SetPlayerOneScore(int score)
    {
        player1Score.text = score.ToString();
    }

    public void SetPlayerTwoScore(int score)
    {
        player2Score.text = score.ToString();
    }
}