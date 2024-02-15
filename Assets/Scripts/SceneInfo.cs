using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneInfo : MonoBehaviour
{
    public Rigidbody Ball;
    public Text ScoreText;
    public GameObject GameOverText;
    public Text HighScoreText;

    private void Awake()
    {
        if (Ball != null)
        {
            MainManager.Instance.Ball = Ball;
        }
        if (ScoreText != null)
        {
            MainManager.Instance.ScoreText = ScoreText;
        }
        if (GameOverText != null)
        {
            MainManager.Instance.GameOverText = GameOverText;
        }
        if (HighScoreText != null)
        {
            MainManager.Instance.highScoreText = HighScoreText;
        }
        MainManager.Instance.AddPoint(0);
        Destroy(gameObject);
    }



}
