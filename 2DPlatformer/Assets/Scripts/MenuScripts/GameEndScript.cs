using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndScript : MonoBehaviour
{
    public GameObject gameEndUI;
    
    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Menu()
    {
        GameObject.Destroy(GameObject.FindGameObjectWithTag("Bolum"));
        SceneManager.LoadScene("Menu");

        gameEndUI.SetActive(false);
    }
    

}
