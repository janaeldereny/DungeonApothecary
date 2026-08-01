using UnityEngine;
using System.IO;
using UnityEngine.SocialPlatforms.Impl;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isGameover;
    public int score;
    public int bestScore;
    

    private string savePath;

    public int hearts=3;
    [SerializeField] private UIhandler uiHandler;
    [SerializeField] public GameObject gameOverPanel;

    

    private void Awake()

    {

        if (Instance == null)

            Instance = this;

        else
        {
            Destroy(gameObject);
            return;
        }
        savePath = Path.Combine(Application.persistentDataPath, "save.json");

        LoadGame();
    }

   private void Start()
{
    score =0;
    uiHandler.UpdateScore(score);
    uiHandler.UpdateBestScore(bestScore);
    uiHandler.UpdateHearts();
}

    public void AddScore(int amount = 1)

    {
        score += amount;
        uiHandler.UpdateScore(score);

        if (score > bestScore)

        {

            bestScore = score;

            uiHandler.UpdateBestScore(bestScore);

            SaveGame();

        }

    }

    public void LoseHeart()

    {
        hearts--;
        uiHandler.UpdateHearts();
        if (hearts < 0)
        {
            Debug.Log ("Game Over");
            isGameover=true;
            GameOver();
        }

    }

    public void GameOver()

    {

        Debug.Log("Game Over");
        gameOverPanel.SetActive(true);

        // uiHandler.ShowGameOver();

        Time.timeScale = 0f;

    }

    private void SaveGame()

    {

        SaveData data = new SaveData();

        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(savePath, json);

    }

    private void LoadGame()

    {

        if (File.Exists(savePath))

        {

            string json = File.ReadAllText(savePath);

            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScore;

        }

        else

        {

            bestScore = 0;

        }

    }

}




  