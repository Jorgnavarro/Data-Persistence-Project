using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    public Text currentPlayerScoreText;
    public Text highScoreText;

    private PlayerData currentPlayer;
    private PlayerData highScorePlayer;


    void Start()
    {
        currentPlayer = SaveSystem.LoadPlayerData();
        highScorePlayer = LoadHighScorePlayerData();

        UpdateUI();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(UpdateScore);
            }
        }

    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public void UpdateScore(int point)
    {
        currentPlayer.score += point;
        SaveSystem.SavePlayerData(currentPlayer);
        UpdateUI();
    }

    private void UpdateUI()
    {
        currentPlayerScoreText.text = $"Player: {currentPlayer.playerName} - Score: {currentPlayer.score}";
        highScoreText.text = $"High Score: {highScorePlayer.playerName} - Score: {highScorePlayer.score}";
        
    }



    public void GameOver()
    {
        m_GameOver = true;
        GameOverText.SetActive(true);

        if (currentPlayer.score > highScorePlayer.score)
        {
            highScorePlayer = new PlayerData
            {
                playerName = currentPlayer.playerName,
                score = currentPlayer.score
            };
            SaveHighScorePlayerData(highScorePlayer);
        }
      
            // Reiniciar el puntaje del jugador si no supera el puntaje más alto
            currentPlayer.score = 0;
            SaveSystem.SavePlayerData(currentPlayer);
        
        UpdateUI();
    }

    private void SaveHighScorePlayerData(PlayerData playerData)
    {
        PlayerPrefs.SetString("HighScorePlayerName", playerData.playerName);
        PlayerPrefs.SetInt("HighScore", playerData.score);
        PlayerPrefs.Save();
    }

    private PlayerData LoadHighScorePlayerData()
    {
        PlayerData playerData = new PlayerData();
        playerData.playerName = PlayerPrefs.GetString("HighScorePlayerName", "No Name");
        playerData.score = PlayerPrefs.GetInt("HighScore", 0);
        return playerData;
    }

}
