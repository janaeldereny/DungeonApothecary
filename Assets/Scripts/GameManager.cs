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
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private PlayerDeath playerDeath;

    

    private void Awake()

    {
        savePath = Path.Combine(Application.persistentDataPath, "save.json");
        if (Instance == null)
        {
            Instance = this;
             LoadGame();   
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        // LoadGame();
    }

   private void Start()
    {
        score =0;
        uiHandler.UpdateScore(score);
        //uiHandler.UpdateBestScore(bestScore);
        
    }

    public void AddScore(int amount = 1)

    {
        score += amount;
        uiHandler.UpdateScore(score);

        // if (score > bestScore)

        // {

        //     bestScore = score;

        //     uiHandler.UpdateBestScore(bestScore);

        //     SaveGame();

        // }

    }

    public void LoseHeart()

    {
        hearts--;
        uiHandler.UpdateHearts();
        if (hearts <= 0)
        {
            Debug.Log ("Game Over");
            isGameover=true;
            playerDeath.Die();
        }

    }

    public void GameOver()

    {

        Debug.Log("Game Over");
        if (score > bestScore)

        {
            bestScore = score;
            SaveGame();
        }

        gameOverScreen.Score(score);
        gameOverScreen.Best(bestScore);
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




  