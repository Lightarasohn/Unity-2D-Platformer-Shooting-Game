using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadSceneScript : MonoBehaviour
{
    public GameObject deadSceneUI;
    public void Menu()
    {
        GameObject.Destroy(GameObject.FindGameObjectWithTag("Bolum"));
        SceneManager.LoadScene("Menu");

        deadSceneUI.SetActive(false);
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
        deadSceneUI.SetActive(false);
    }
}
