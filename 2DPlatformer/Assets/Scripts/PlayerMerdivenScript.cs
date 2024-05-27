using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMerdivenScript : MonoBehaviour
{
    public bool isPlayerOnPlatform;
    public bool isPlayerTriggerStair;

    void Start()
    {
        
    }

    void Update()
    {
        //Debug.Log(isPlayerTriggerStair);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Debug.Log(collision.transform.tag);
        if (collision.transform.tag == "MerdivenUstuTag")
        {
            isPlayerOnPlatform = true;
        }
        else
        {
            isPlayerOnPlatform = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if(collision.tag == "MerdivenAltiTag")
        {
            isPlayerTriggerStair = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MerdivenAltiTag")
        {
            isPlayerTriggerStair = false;
        }
    }
}
