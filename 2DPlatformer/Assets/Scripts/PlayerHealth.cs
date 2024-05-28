using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 200f;
    public float currentHealth;
    public HealthBarScript healthBar;
    private Animator an;
   
    public void hurtPlayer(float damage)
    {
        currentHealth -= damage;
        healthBar.setHealth(currentHealth);
    }
    public bool isPlayerDead()
    {
        return currentHealth <= 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        an = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerDead())
        {
            if (an != null) // Check if Animator exists
            {
                an.SetTrigger("Death"); // Trigger the Death animation
            }
            GameObject.Destroy(gameObject);
            GameObject.FindGameObjectWithTag("DeadMenu").transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
