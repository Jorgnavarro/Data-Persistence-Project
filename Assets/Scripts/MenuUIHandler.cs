using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIHandler : MonoBehaviour
{
    public TMP_InputField inputField;

    private void Start()
    {
        
    }

    public void StartGame()
    {
        string playerName = inputField.text;
        Debug.Log(playerName);
        PlayerData currentPlayer = new PlayerData { playerName = playerName, score = 0 };
        SaveSystem.SavePlayerData(currentPlayer);
        SceneManager.LoadScene("Main");
    }
}
