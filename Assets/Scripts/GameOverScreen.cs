using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameOverScreen : MonoBehaviour
{
    
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestText;

   public void Score(int score)
   {
       gameObject.SetActive(true);
        scoreText.text = "Score: " + score.ToString() +" Points";
   }

   public void Best(int best)
   {
       bestText.text = "Best: " + best.ToString() +" Points";
   }

   public void Restart()
   {
    Time.timeScale = 1f;
    SceneManager.LoadScene("MainScene");
   }


    public void MainMenu()
   {
        Time.timeScale = 1f;
       SceneManager.LoadScene("Main Menu");
   }

   public void Show(int score, int bestScore)
    {
         Score(score);
        Best(bestScore);
        gameObject.SetActive(true);
    }
}
