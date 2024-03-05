using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashControling : MonoBehaviour
{
    [SerializeField] private GameObject karakter;
    private bool dashControl;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dashControl = karakter.GetComponent<PlayerMovement>().isDashing;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == karakter)
        {
            if (!dashControl)
            { 
                Debug.Log("Carpildi");

            }
        }
    }
}
