using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject pausePanel;

    public bool isPaused = false;

    public void ResumeGame()

    {
        pausePanel.SetActive(false);

        Time.timeScale = 1f;

        isPaused = false;

    }

    public void HomeMenu()

    {
        Time.timeScale = 1f;

        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");

    }

     public void PauseGame()

    {
        pausePanel.SetActive(true);

        Time.timeScale = 0f;

        isPaused = true;
    }

}
