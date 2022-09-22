using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLife : MonoBehaviour
{
    private Rigidbody2D rb;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("trap"))
        {
            DestoryItem();
        }
    }

    private void DestoryItem()
    {
        rb.bodyType = RigidbodyType2D.Static;
        Destroy(gameObject);
    }
}
