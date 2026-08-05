using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isGameover;
    public int score;
    public int bestScore;


    public int hearts=3;
    [SerializeField] private UIhandler uiHandler;
    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private PlayerDeath playerDeath;
    [SerializeField] private SaveSystem saveSystem;

    

    private void Awake()

    {
        
        if (Instance == null)
        {
            Instance = this;
            bestScore = saveSystem.LoadGame();   
        }
        else
        {
            Destroy(gameObject);
            return;
        }
       
    }

   private void Start()
    {
        score =0;
        uiHandler.UpdateScore(score);
       
        
    }

    public void AddScore(int amount = 1)

    {
        score += amount;
        uiHandler.UpdateScore(score);
        UpdateBestScore();

    }

    private void UpdateBestScore()
    {
         if (score > bestScore)
        {
            bestScore = score;
            saveSystem.SaveGame(bestScore);
        } 
    }


    public void LoseHeart()

    {
        hearts--;
        uiHandler.UpdateHearts();
        if (hearts <= 0)
        {
            isGameover=true;
            playerDeath.Die();
        }

    }

    public void GameOver()

    {

        Debug.Log("Game Over");
        gameOverScreen.Show(score , bestScore);
        Time.timeScale = 0f;

    }

    

}




  