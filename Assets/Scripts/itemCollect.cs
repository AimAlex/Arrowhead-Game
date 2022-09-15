using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class itemCollect : MonoBehaviour
{
    private int itemNumber = 0;
    private SpriteRenderer _renderer;
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
                _renderer.color = Color.black; 
                Destroy(col.gameObject);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
