using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class MainManager : MonoBehaviour
{

    public static MainManager Instance;

    public Brick BrickPrefab;

    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    public string playerName = "Player";
    public playerNameInput m_playerNameInput;
    public int highScore = 0;
    public Text highScoreText;

    private bool m_Started = false;
    public int m_Points;
    
    public bool m_GameOver = false;

    private void Awake()
    {

        if (SceneManager.GetActiveScene().name == "main")
        {
            StartGame();
        }

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    private void Update()
    {
        

        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_GameOver = false;
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = false;
                m_Points = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void AddPoint(int point)
    {
        m_Points += point;
        if (m_Points > highScore)
        {
            highScore = m_Points;
        }
        ScoreText.text = $"Score : {m_Points}";
        highScoreText.text = ($"Best Score : {playerName} : {highScore}");
    }

    public void GameOver()
    {
        SaveGame();
        m_GameOver = true;
        GameOverText.SetActive(true);

    }


    [System.Serializable]
    class SaveData
    {
        public int s_highscore;
        public string s_playerName;
    }

    public void SaveGame()
    {
        SaveData data = new SaveData();
        data.s_highscore = highScore; //Save the score of the player
        data.s_playerName = playerName; //Save the name of the player

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    }

    public void LoadGame()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.s_playerName;
            highScore = data.s_highscore;
        }

        m_playerNameInput.LoadPlayerName();
        highScoreText.text = ($"Best Score : {playerName} : {highScore}");


    }

    public void SetPlayerName(string name)
    {
        playerName = name;
        SaveGame();
    }

    public void StartGame()
    {


        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }
}
