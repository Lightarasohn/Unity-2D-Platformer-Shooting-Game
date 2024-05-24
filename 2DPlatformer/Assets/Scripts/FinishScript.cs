using UnityEngine;

public class FinishScript : MonoBehaviour
{
    private GameObject[] rangedEnemies;
    private GameObject[] meleeEnemies;
    void Start()
    {

    }

    void Update()
    {
        rangedEnemies = GameObject.FindGameObjectsWithTag("RangedEnemy");
        meleeEnemies = GameObject.FindGameObjectsWithTag("MeleeEnemy");
        
        if(rangedEnemies.Length + meleeEnemies.Length == 0)
        {
            transform.GetComponent<BoxCollider2D>().enabled = true;
            transform.GetComponent<SpriteRenderer>().enabled = true;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Time.timeScale = 0f;
            GameObject.FindGameObjectWithTag("PauseMenu").transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}