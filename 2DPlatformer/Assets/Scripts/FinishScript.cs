using UnityEngine;

public class FinishScript : MonoBehaviour
{
    private GameObject[] rangedEnemies;
    private GameObject[] meleeEnemies;
    private GameObject[] bossEnemies;
    private AudioClip levelEndAudio;
    private AudioSource audioSource;
    void Start()
    {
        levelEndAudio = Resources.Load<AudioClip>("SoundEffects/GameAndMenuSounds/LevelEnd");
        audioSource = transform.GetComponent<AudioSource>();
        audioSource.clip = levelEndAudio;
    }

    void Update()
    {
        bossEnemies = GameObject.FindGameObjectsWithTag("BossEnemy");
        rangedEnemies = GameObject.FindGameObjectsWithTag("RangedEnemy");
        meleeEnemies = GameObject.FindGameObjectsWithTag("MeleeEnemy");
        
        if(rangedEnemies.Length + meleeEnemies.Length + bossEnemies.Length == 0)
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
            audioSource.Play();
            GameObject.FindGameObjectWithTag("PauseMenu").transform.GetChild(1).gameObject.SetActive(true);
        }
    }
}