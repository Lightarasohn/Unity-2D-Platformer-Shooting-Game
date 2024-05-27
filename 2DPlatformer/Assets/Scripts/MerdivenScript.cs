using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MerdivenScript : MonoBehaviour
{
    private bool isPlayerTriggerStair;
    private bool isPlayerOnPlatform;
    private Transform player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {

        if (GameObject.FindGameObjectWithTag("Player") != null)
        {

            isPlayerOnPlatform = player.GetComponent<PlayerMerdivenScript>().isPlayerOnPlatform;
            isPlayerTriggerStair = player.GetComponent<PlayerMerdivenScript>().isPlayerTriggerStair;
            if (isPlayerTriggerStair)
            {
                transform.GetComponent<TilemapCollider2D>().enabled = false;
            }
            else if (!isPlayerOnPlatform)
            {
                transform.GetComponent<TilemapCollider2D>().enabled = true;
            }
            else if (isPlayerOnPlatform && player.GetComponent<PlayerMovement>().isGrounded() && Input.GetKey(KeyCode.S))
            {
                //Debug.Log("Dustu");
                transform.GetComponent<TilemapCollider2D>().enabled = false;

            }
        }
    }
}
