using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused) {
                Resume();
            }
            else
            {
                Pause();
            }
        }   
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

    }
    public void Menu()
    {
        GameObject.Destroy(GameObject.FindGameObjectWithTag("Bolum"));
        SceneManager.LoadScene("Menu");

        pauseMenuUI.SetActive(false);
        GameIsPaused = true;
    }
    public void Quit()
    {
        Application.Quit();
    }
}


