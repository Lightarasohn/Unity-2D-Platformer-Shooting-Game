using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
    private GameObject[] backgroundChilds;
    // Start is called before the first frame update
    void Start()
    {
        backgroundChilds = GameObject.FindGameObjectsWithTag("BackGroundChild");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                foreach (GameObject child in backgroundChilds)
                {
                    child.transform.GetComponent<BackgroundScript>().speedOfParallax /= 2;
                }
                Time.timeScale = 0.5f;
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    foreach (GameObject child in backgroundChilds)
                    {
                        child.transform.GetComponent<BackgroundScript>().speedOfParallax *= 2;
                    }
                    Time.timeScale = 1f;
                }
            }
        }
    }
}
