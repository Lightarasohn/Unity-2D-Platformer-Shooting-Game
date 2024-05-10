using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedCombat : MonoBehaviour
{
    [SerializeField]
    GameObject bullet;

    [SerializeField]
    Transform bulletPosition;

    [SerializeField]
    Transform castPoint;

    [SerializeField]
    EnemyAgroScript enemyScript;
    
    private float timer = 2.0f;
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (enemyScript.canSeePlayer(enemyScript.agroRange))
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }
    }

    void shoot()
    {
        Instantiate(bullet, bulletPosition.position, Quaternion.identity);
    }
    
}
