using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemCollect : MonoBehaviour
{
    private int itemNumber = 0;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Treasure"))
        {
            ++itemNumber;
            Destroy(col.gameObject);
        }

        if (col.CompareTag("Finish"))
        {
            if (itemNumber == 4)
            {
                // succeed
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
