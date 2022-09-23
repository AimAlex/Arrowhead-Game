using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clonedItemScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(!col.CompareTag("Player")){
            Destroy(gameObject.GetComponent<Rigidbody2D>());
        }
    }
}
