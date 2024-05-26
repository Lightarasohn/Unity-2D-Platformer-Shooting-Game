using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SlowTime : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Time.timeScale = 0.5f;
            }
            else
            {
                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    Time.timeScale = 1f;
                }
            }
        }
    }
}
