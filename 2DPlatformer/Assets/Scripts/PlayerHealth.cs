using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health = 200f;
    private Animator an;

    public void hurtPlayer(float damage)
    {
        health -= damage;
    }
    public bool isPlayerDead()
    {
        return health <= 0;
    }
    // Start is called before the first frame update
    void Start()
    {
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
            GameObject.FindGameObjectWithTag("DeadMenu").transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Destroy(gameObject);
        }
    }
}
