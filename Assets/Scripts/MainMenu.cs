using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        Time.timeScale = 1f;
         SceneManager.LoadScene("MainScene");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Actual Game Should Be Closed");
    }
}
