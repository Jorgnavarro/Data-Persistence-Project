using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class HighScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        PlayerData highScorePlayer = LoadHighScorePlayerData();

        if(highScorePlayer != null)
        {
            highScoreText.text = $"High Score: {highScorePlayer.playerName} - Score: {highScorePlayer.score}";
        }
        else
        {
            highScoreText.text = "No high score yet!";
        }
    }

    private PlayerData LoadHighScorePlayerData()
    {
        PlayerData playerData = new PlayerData();
        playerData.playerName = PlayerPrefs.GetString("HighScorePlayerName", "No Name");
        playerData.score = PlayerPrefs.GetInt("HighScore", 0);
        return playerData;
    }
}
