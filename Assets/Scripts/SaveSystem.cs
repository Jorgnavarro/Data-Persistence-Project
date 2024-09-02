using System.IO;
using UnityEngine;
public static class SaveSystem 
{
    private static string path = Application.persistentDataPath + "/highscore.json";

    public static void SavePlayerData(PlayerData data)
    {
        Debug.Log("Saving to: " + path);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    public static PlayerData LoadPlayerData()
    {
        Debug.Log("Loading from: " + path);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            return JsonUtility.FromJson<PlayerData>(json);
        }
        return null;
    }
}
