using UnityEngine;
using System.IO;


public class SaveSystem : MonoBehaviour
{  
    private string savePath;


    private void Awake()
    {
     savePath = Path.Combine(Application.persistentDataPath, "save.json");
    }
    public void SaveGame(int bestScore)
    {
        SaveData data = new SaveData();

        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(savePath, json);

    }

    public int LoadGame()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            return data.bestScore;
        }
        return 0;
    }
}
