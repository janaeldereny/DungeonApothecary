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
        scoreText.text = score.ToString() +" Points";
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
       SceneManager.LoadScene("Main Menu");
   }
}
