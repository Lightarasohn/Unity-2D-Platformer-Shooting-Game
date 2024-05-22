using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float health = 200f;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerDead())
        {
            GameObject.Destroy(gameObject);
        }
    }
}
