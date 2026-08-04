using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject pausePanel;

    public bool isPaused = false;

    // private void Update()

    // {

    //     if (Input.GetKeyDown(KeyCode.Escape))

    //     {
    //         if (isPaused)

    //             ResumeGame();

    //         else

    //             PauseGame();

    //     }

    // }


    public void ResumeGame()

    {
        pausePanel.SetActive(false);

        Time.timeScale = 1f;

        isPaused = false;

    }

    public void RestartLevel()

    {
        Time.timeScale = 1f;

        UnityEngine.SceneManagement.SceneManager.LoadScene(

            UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex

        );

    }

    public void HomeMenu()

    {
        Time.timeScale = 1f;

        UnityEngine.SceneManagement.SceneManager.LoadScene("Main Menu");

    }

}
