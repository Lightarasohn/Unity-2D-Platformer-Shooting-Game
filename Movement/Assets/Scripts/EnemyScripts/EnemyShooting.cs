using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private Transform bulletPosition;

    private float timer;
    private float shootingRange = 20.0f;
    private bool isFacingLeft = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canSeePlayer(shootingRange))
        {
            timer += Time.deltaTime;
            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }
    }

    private void shoot()
    {
        Instantiate(bullet, bulletPosition.position, Quaternion.identity);
    }

    private bool canSeePlayer(float distance)
    {
        bool tmp = false;
        float castDist;

        if (isFacingLeft)
        {
            castDist = -distance;
        }
        else
        {
            castDist = distance;
        }

        Vector2 endPosition = bulletPosition.position + Vector3.right * castDist;
        RaycastHit2D hit = Physics2D.Linecast(bulletPosition.position, endPosition, 1 << LayerMask.NameToLayer("ActionLayer"));

        if (hit.collider != null)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                tmp = true;
            }
            else
            {
                tmp = false;
            }
        }
        return tmp;
    }
}
